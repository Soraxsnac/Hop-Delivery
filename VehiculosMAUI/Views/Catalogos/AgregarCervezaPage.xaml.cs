using VehiculosMAUI.DTOs;
using VehiculosMAUI.Services.HttpServices;

namespace VehiculosMAUI.Views;

public partial class AgregarCervezaPage : ContentPage
{
    private readonly IApiService _apiService;

    public AgregarCervezaPage(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        try
        {
            // Validamos números
            double.TryParse(ABVEntry.Text, out double abv);
            int.TryParse(IBUEntry.Text, out int ibu);

            var nuevaCerveza = new CervezaDTO
            {
                Nombre = NombreEntry.Text,
                Tipo = TipoEntry.Text,
                ABV = abv,
                IBU = ibu,
                Descripcion = DescripcionEditor.Text,
                ImagenURL = ImagenEntry.Text ?? "https://img.freepik.com/vector-premium/vaso-cerveza-oscura-ilustracion-vectorial_7243-228.jpg"
            };

            // Intentamos enviar al servidor
            bool exito = await _apiService.CrearCervezaAsync(nuevaCerveza);

            if (exito)
            {
                await DisplayAlert("Éxito", "Cerveza guardada correctamente", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo guardar la cerveza en la base de datos.", "OK");
            }
        }
        catch (Exception ex)
        {
          
            await DisplayAlert("Crash Evitado", $"El servidor rechazó la conexión. Detalle técnico:\n\n{ex.Message}", "Entendido");
        }
    }
}