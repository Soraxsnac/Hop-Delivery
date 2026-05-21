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

    // Evento que nos lleva a la pantalla de crear
    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AgregarCervezaPage));
    }
}