using VehiculosMAUI.DTOs;
using VehiculosMAUI.Services.HttpServices;

namespace VehiculosMAUI.Views;

public partial class LoginPage : ContentPage
{
    private readonly IApiService apiService;

    public LoginPage(IApiService apiService)
    {
        InitializeComponent();
        this.apiService = apiService;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        UserCredentialsDTO credentials = new UserCredentialsDTO
        {
            username = txtUsuario.Text,
            password = txtPassword.Text
        };

        var respuesta = await apiService.PostAsync<TokenDTO>("auth/login", credentials);

        if (respuesta != null)
        {
            lblToken.Text = $"Token: {respuesta.token}";
            await SecureStorage.SetAsync("TokenApp", respuesta.token);
        }
    }
   
}