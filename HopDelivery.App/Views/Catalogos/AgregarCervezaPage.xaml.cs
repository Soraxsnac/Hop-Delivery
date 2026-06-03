using HopDelivery.DTOs;
using HopDelivery.Services.HttpServices;

namespace HopDelivery.Views.Catalogos
{
    public partial class AgregarCervezaPage : ContentPage
    {
        private readonly IApiService _apiService;

        public AgregarCervezaPage()
        {
            InitializeComponent();
            _apiService = IPlatformApplication.Current.Services.GetService<IApiService>();
            PickerTipo.ItemsSource = new string[] { "Lager", "IPA", "Stout / Porter" };
            PickerMarca.ItemsSource = new string[] { "Grupo Modelo", "Heineken México", "Guinness", "Artesanal / Otra" };
            PickerCalificacion.ItemsSource = new string[] { "0 Estrellas", "1 Estrella", "2 Estrellas", "3 Estrellas", "4 Estrellas", "5 Estrellas" };
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            var nuevaCerveza = new CervezaDTO
            {
                Nombre = TxtNombre.Text?.Trim(),
                Tipo = PickerTipo.SelectedItem?.ToString() ?? "Lager",
                ABV = double.TryParse(TxtABV.Text, out var abv) ? abv : 0.0,
                IBU = int.TryParse(TxtIBU.Text, out var ibu) ? ibu : 0,
                Descripcion = TxtDescripcion.Text ?? "",
                ImagenURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/42/Beer_tasting.jpg/800px-Beer_tasting.jpg",
                IdMarca = PickerMarca.SelectedIndex + 1,
                Calificacion = PickerCalificacion.SelectedIndex
            };

            if (await _apiService.CrearCervezaAsync(nuevaCerveza)) await Navigation.PopAsync();
            else await DisplayAlert("Error", "No se pudo guardar", "OK");
        }
    }
}