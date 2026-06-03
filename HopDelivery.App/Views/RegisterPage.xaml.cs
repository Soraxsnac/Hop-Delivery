using HopDelivery.App.DTOs;
using HopDelivery.DTOs;
using HopDelivery.Services.HttpServices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;

namespace HopDelivery.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly IApiService _apiService;

        public RegisterPage(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OnRegistrarUsuarioClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtEmail.Text) || string.IsNullOrWhiteSpace(TxtPassword.Text))
            {
                await DisplayAlert("Error", "Por favor llena todos los campos", "OK");
                return;
            }

            if (TxtPassword.Text != TxtConfirmPassword.Text)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            var nuevoUsuario = new UserCredentialsDTO
            {
                Email = TxtEmail.Text,
                Password = TxtPassword.Text
            };

            try
            {
                var tokenResult = await _apiService.PostAsync<TokenDTO>("Auth/register", nuevoUsuario);

                if (tokenResult != null && !string.IsNullOrEmpty(tokenResult.Token))
                {
                    await SecureStorage.SetAsync("auth_token", tokenResult.Token);
                    await DisplayAlert("Éxito", "Cuenta creada correctamente", "OK");

                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo crear la cuenta. Intenta con otro correo.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Revisa que la API esté corriendo.", "OK");
                Console.WriteLine($"Error Registro: {ex.Message}");
            }
        }
    }
}