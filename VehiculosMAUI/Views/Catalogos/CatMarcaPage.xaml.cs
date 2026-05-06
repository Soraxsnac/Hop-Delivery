using VehiculosMAUI.DTOs;
using VehiculosMAUI.Services.HttpServices;

namespace VehiculosMAUI.Views.Catalogos;

public partial class CatMarcaPage : ContentPage
{
    private readonly IApiService apiService;

    public CatMarcaPage(IApiService apiService)
	{
		InitializeComponent();
        this.apiService = apiService;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var crearCatMarcaDTO = new CrearCatMarcaDTO
        {
            Marca = txtMarca.Text
        };

        var response = await apiService.PostAsync<CrearCatMarcaDTO>("catalogos/nuevamarca", crearCatMarcaDTO);


        //await DisplayAlert("Error", "No se pudo crear la marca", "OK");
    }
}