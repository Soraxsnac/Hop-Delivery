using VehiculosMAUI.DTOs;
using VehiculosMAUI.Services.HttpServices;
using VehiculosMAUI.Views;

namespace VehiculosMAUI;

public partial class MainPage : ContentPage
{
    private readonly IApiService _apiService;

    public MainPage(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarCervezasAsync();
    }

    private async Task CargarCervezasAsync()
    {
        var cervezas = await _apiService.ObtenerCervezasAsync();
        CervezasCollection.ItemsSource = cervezas;
    }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AgregarCervezaPage));
    }

    // Nuevo evento de interacción para Editar/Eliminar
    private async void OnCervezaSeleccionada(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not CervezaDTO cervezaSeleccionada)
            return;

        CervezasCollection.SelectedItem = null;

        string accion = await DisplayActionSheet($"Opciones para {cervezaSeleccionada.Nombre}", "Cancelar", "Eliminar", "Editar");

        if (accion == "Eliminar")
        {
            bool confirmar = await DisplayAlert("Peligro", $"¿Eliminar {cervezaSeleccionada.Nombre} definitivamente?", "Sí, eliminar", "No");
            if (confirmar)
            {
                bool exito = await _apiService.EliminarCervezaAsync(cervezaSeleccionada.Id);
                if (exito)
                {
                    await CargarCervezasAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar la cerveza.", "OK");
                }
            }
        }
        else if (accion == "Editar")
        {
            var parametros = new Dictionary<string, object>
            {
                { "CervezaAEditar", cervezaSeleccionada }
            };

            await Shell.Current.GoToAsync(nameof(AgregarCervezaPage), parametros);
        }
    }
}