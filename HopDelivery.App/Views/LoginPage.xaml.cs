using System;
using Microsoft.Maui.Controls;
using HopDelivery.DTOs;
using HopDelivery.Services.HttpServices;

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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Tu lógica de inicio de sesión se queda aquí adentro
        }
    }
}