using System;
using Microsoft.Maui.Controls;
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
            PickerCalificacion.ItemsSource = new string[] { "0 Estrellas (Pésima)", "1 Estrella", "2 Estrellas", "3 Estrellas (Regular)", "4 Estrellas", "5 Estrellas (Excelente)" };
        }

        // Esta función evalúa la marca y devuelve el link seguro de Wikipedia
        private string ObtenerUrlImagenSegura(int index)
        {
            if (index == 0) return "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Corona-6Pack.JPG/800px-Corona-6Pack.JPG";
            if (index == 1) return "https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Heineken_pils_bottle.png/400px-Heineken_pils_bottle.png";
            if (index == 2) return "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/Pint_of_Guinness_Mac.jpg/400px-Pint_of_Guinness_Mac.jpg";

            // Si es artesanal o cualquier otra
            return "https://upload.wikimedia.org/wikipedia/commons/thumb/4/42/Beer_tasting.jpg/800px-Beer_tasting.jpg";
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombre.Text))
            {
                await DisplayAlert("Aviso", "El nombre es obligatorio.", "OK");
                return;
            }

            var nuevaCerveza = new CervezaDTO
            {
                Nombre = TxtNombre.Text.Trim(),
                Tipo = PickerTipo.SelectedIndex >= 0 ? PickerTipo.SelectedItem.ToString() : "Lager",
                ABV = double.TryParse(TxtABV.Text, out var abv) ? abv : 0.0,
                IBU = int.TryParse(TxtIBU.Text, out var ibu) ? ibu : 0,
                Descripcion = TxtDescripcion.Text?.Trim() ?? "",

                // Inyectamos la imagen calculada justo en el momento de armar el paquete
                ImagenURL = ObtenerUrlImagenSegura(PickerMarca.SelectedIndex),

                IdMarca = PickerMarca.SelectedIndex >= 0 ? (PickerMarca.SelectedIndex + 1) : 4,
                Calificacion = PickerCalificacion.SelectedIndex >= 0 ? PickerCalificacion.SelectedIndex : 0
            };

            try
            {
                var exito = await _apiService.CrearCervezaAsync(nuevaCerveza);
                if (exito)
                {
                    await DisplayAlert("¡Éxito!", "Cerveza agregada.", "OK");
                    await Navigation.PopAsync();
                }
                else await DisplayAlert("Error", "La API rechazó los datos.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Conexión: {ex.Message}", "OK");
            }
        }
    }
}