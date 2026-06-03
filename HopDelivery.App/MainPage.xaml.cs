using Microsoft.Maui.Controls;
using HopDelivery.Services.HttpServices;
using HopDelivery.Views.Catalogos;

namespace HopDelivery.App
{
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
            await CargarCervezas();
        }

        private async Task CargarCervezas()
        {
            try { ListaCervezas.ItemsSource = await _apiService.ObtenerCervezasAsync(); }
            catch (Exception ex) { await DisplayAlert("Error", ex.Message, "OK"); }
        }

        private async void OnAgregarClicked(object sender, EventArgs e) => await Navigation.PushAsync(new AgregarCervezaPage());

        private async void OnEditarClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var cerveza = btn.CommandParameter as HopDelivery.DTOs.CervezaDTO;
            if (cerveza != null) await Navigation.PushAsync(new EditarCervezaPage(cerveza));
        }
    }
}