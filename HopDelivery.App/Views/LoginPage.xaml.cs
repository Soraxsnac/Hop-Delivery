using Microsoft.Maui.Controls;
using HopDelivery.Services.HttpServices;

namespace HopDelivery.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage() => InitializeComponent();

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (TxtUser.Text == "admin" && TxtPass.Text == "1234")
            {
                // Inyectamos el servicio y navegamos a la pantalla principal
                var apiService = IPlatformApplication.Current.Services.GetService<IApiService>();
                Application.Current.MainPage = new NavigationPage(new HopDelivery.App.MainPage(apiService));
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
        }
    }
}