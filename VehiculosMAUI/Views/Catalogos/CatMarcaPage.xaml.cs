using VehiculosMAUI.DTOs;
using VehiculosMAUI.Services.HttpServices;

namespace VehiculosMAUI.Views.Catalogos;

public partial class CatMarcaPage : ContentPage
{
    private readonly IApiService _apiService;

    public CatMarcaPage(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarMarcasAsync();
    }

    private async Task CargarMarcasAsync()
    {
        var marcas = await _apiService.ObtenerMarcasAsync();
        MarcasCollection.ItemsSource = marcas;
    }

    // Botón para CREAR (Abre un Pop-up)
    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        string resultado = await DisplayPromptAsync("Nueva Marca", "Ingresa el nombre de la marca:", "Guardar", "Cancelar", "Ej. Corona");

        if (!string.IsNullOrWhiteSpace(resultado))
        {
            var dto = new CrearCatMarcaDTO { Marca = resultado };
            bool exito = await _apiService.CrearMarcaAsync(dto);

            if (exito) await CargarMarcasAsync();
            else await DisplayAlert("Error", "No se pudo guardar la marca.", "OK");
        }
    }

    // Tocar una marca para EDITAR o ELIMINAR
    private async void OnMarcaSeleccionada(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not MarcaDTO marcaSeleccionada)
            return;

        MarcasCollection.SelectedItem = null; // Quitar selección visual

        string accion = await DisplayActionSheet($"Opciones para {marcaSeleccionada.Marca}", "Cancelar", "Eliminar", "Editar Nombre");

        if (accion == "Eliminar")
        {
            bool confirmar = await DisplayAlert("Confirmar", $"¿Eliminar {marcaSeleccionada.Marca}?", "Sí", "No");
            if (confirmar)
            {
                bool exito = await _apiService.EliminarMarcaAsync(marcaSeleccionada.Id);
                if (exito) await CargarMarcasAsync();
            }
        }
        else if (accion == "Editar Nombre")
        {
            // Abre un Pop-up con el nombre actual ya escrito
            string nuevoNombre = await DisplayPromptAsync("Editar Marca", "Modifica el nombre:", "Actualizar", "Cancelar", null, -1, keyboard: null, marcaSeleccionada.Marca);

            if (!string.IsNullOrWhiteSpace(nuevoNombre) && nuevoNombre != marcaSeleccionada.Marca)
            {
                var dto = new CrearCatMarcaDTO { Marca = nuevoNombre };
                bool exito = await _apiService.ActualizarMarcaAsync(marcaSeleccionada.Id, dto);

                if (exito) await CargarMarcasAsync();
            }
        }
    }
}