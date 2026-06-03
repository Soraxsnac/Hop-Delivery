using System;
using Microsoft.Maui.Controls;
using HopDelivery.Services.HttpServices;

namespace HopDelivery
{
    public partial class MainPage : ContentPage
    {
        private readonly IApiService _apiService;

        public MainPage(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OnRefrescarClicked(object sender, EventArgs e)
        {
            var boton = (Button)sender;
            boton.Text = "Conectando...";
            boton.IsEnabled = false;

            try
            {
                // Llamamos a tu método real con DTOs
                var cervezas = await _apiService.ObtenerCervezasAsync();

                await DisplayAlert("¡Conexión Exitosa!", $"Los datos llegaron desde la API. Se encontraron {cervezas.Count} cervezas.", "Excelente");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Oculto", $"Esto es lo que falló:\n\n{ex.Message}", "OK");
            }
            finally
            {
                boton.Text = "Refrescar Catálogo";
                boton.IsEnabled = true;
            }
        }
    }
}