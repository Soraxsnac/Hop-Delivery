using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using HopDelivery.Services.HttpServices;
using HopDelivery.Views.Catalogos; // <--- Línea crucial para enlazar las vistas

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

        private async void OnRefrescarClicked(object sender, EventArgs e)
        {
            BtnRefrescar.Text = "Cargando...";
            BtnRefrescar.IsEnabled = false;

            await CargarCervezas();

            BtnRefrescar.Text = "Refrescar Catálogo";
            BtnRefrescar.IsEnabled = true;
        }

        private async Task CargarCervezas()
        {
            try
            {
                var cervezas = await _apiService.ObtenerCervezasAsync();

                if (cervezas == null || cervezas.Count == 0)
                {
                    await DisplayAlert("Aviso", "No hay cervezas registradas en la base de datos.", "OK");
                }

                ListaCervezas.ItemsSource = cervezas;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo conectar a la API: {ex.Message}", "OK");
            }
        }

        private async void OnAgregarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarCervezaPage());
        }

        private async void OnEditarClicked(object sender, EventArgs e)
        {
            var boton = sender as Button;

            // Mandamos la cerveza completa a la pantalla de edición
            var cervezaSeleccionada = boton.CommandParameter as HopDelivery.DTOs.CervezaDTO;

            if (cervezaSeleccionada != null)
            {
                await Navigation.PushAsync(new EditarCervezaPage(cervezaSeleccionada));
            }
        }
    }
}