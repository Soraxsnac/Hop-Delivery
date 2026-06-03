using HopDelivery.App.DTOs;
using HopDelivery.DTOs;
using HopDelivery.Services.HttpServices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;

namespace HopDelivery.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly IApiService _apiService;

        public LoginPage(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtUser.Text) || string.IsNullOrWhiteSpace(TxtPass.Text))
            {
                await DisplayAlert("Error", "Por favor llena todos los campos", "OK");
                return;
            }

            var credenciales = new UserCredentialsDTO
            {
                Email = TxtUser.Text,
                Password = TxtPass.Text
            };

            try
            {
                var tokenResult = await _apiService.PostAsync<TokenDTO>("Auth/login", credenciales);

                if (tokenResult != null && !string.IsNullOrEmpty(tokenResult.Token))
                {
                    await SecureStorage.SetAsync("auth_token", tokenResult.Token);
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Acceso Denegado", "Usuario o contraseña incorrectos", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo conectar a la API.", "OK");
                Console.WriteLine($"Error Login: {ex.Message}");
            }
        }

        private async void OnIrARegistroClicked(object sender, EventArgs e)
        {
            // Navegamos al registro pasando el servicio API
            await Navigation.PushAsync(new RegisterPage(_apiService));
        }
    }
}