using VehiculosMAUI.DTOs;
using VehiculosMAUI.Services.HttpServices;

namespace VehiculosMAUI.Views;

[QueryProperty(nameof(CervezaAEditar), "CervezaAEditar")]
public partial class AgregarCervezaPage : ContentPage
{
    private readonly IApiService _apiService;
    private CervezaDTO _cervezaAEditar;

    public CervezaDTO CervezaAEditar
    {
        get => _cervezaAEditar;
        set
        {
            _cervezaAEditar = value;
            CargarDatosEnPantalla();
        }
    }

    public AgregarCervezaPage(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private void CargarDatosEnPantalla()
    {
        if (_cervezaAEditar != null)
        {
            Title = "Editar Cerveza";
            NombreEntry.Text = _cervezaAEditar.Nombre;
            TipoEntry.Text = _cervezaAEditar.Tipo;
            ABVEntry.Text = _cervezaAEditar.ABV.ToString();
            IBUEntry.Text = _cervezaAEditar.IBU.ToString();
            DescripcionEditor.Text = _cervezaAEditar.Descripcion;
            ImagenEntry.Text = _cervezaAEditar.ImagenURL;
        }
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        try
        {
            double.TryParse(ABVEntry.Text, out double abv);
            int.TryParse(IBUEntry.Text, out int ibu);

            int idActual = _cervezaAEditar != null ? _cervezaAEditar.Id : 0;

            var cerveza = new CervezaDTO
            {
                Id = idActual,
                Nombre = NombreEntry.Text,
                Tipo = TipoEntry.Text,
                ABV = abv,
                IBU = ibu,
                Descripcion = DescripcionEditor.Text,
                ImagenURL = string.IsNullOrWhiteSpace(ImagenEntry.Text) ? "https://img.freepik.com/vector-premium/vaso-cerveza-oscura-ilustracion-vectorial_7243-228.jpg" : ImagenEntry.Text
            };

            bool exito;

            if (idActual == 0)
            {
                exito = await _apiService.CrearCervezaAsync(cerveza);
            }
            else
            {
                exito = await _apiService.ActualizarCervezaAsync(idActual, cerveza);
            }

            if (exito)
            {
                await DisplayAlert("Éxito", "Operación completada correctamente", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo guardar la cerveza en la base de datos.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Crash Evitado", $"El servidor rechazó la conexión. Detalle:\n\n{ex.Message}", "Entendido");
        }
    }
}