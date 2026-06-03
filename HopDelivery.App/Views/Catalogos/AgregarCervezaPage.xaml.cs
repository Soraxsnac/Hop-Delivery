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

            // Resolvemos la conexión a la API directamente
            _apiService = IPlatformApplication.Current.Services.GetService<IApiService>();
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Validación básica para evitar enviar datos nulos
            if (string.IsNullOrWhiteSpace(TxtNombre.Text))
            {
                await DisplayAlert("Aviso", "El nombre de la cerveza es obligatorio.", "OK");
                return;
            }

            // Obtenemos el valor numérico del Picker de estrellas (de 0 a 5)
            int estrellas = PickerCalificacion.SelectedIndex >= 0 ? PickerCalificacion.SelectedIndex : 0;

            // Construimos el DTO con los nombres exactos que exige tu API
            var nuevaCerveza = new CervezaDTO
            {
                Nombre = TxtNombre.Text.Trim(),
                Tipo = TxtTipo.Text?.Trim() ?? "General",
                ABV = double.TryParse(TxtABV.Text, out var abv) ? abv : 0.0,
                IBU = int.TryParse(TxtIBU.Text, out var ibu) ? ibu : 0,
                Descripcion = TxtDescripcion.Text?.Trim() ?? "",
                ImagenURL = "",
                IdMarca = int.TryParse(TxtIdMarca.Text, out var idMarca) ? idMarca : 1,
                Calificacion = estrellas
            };

            try
            {
                var exito = await _apiService.CrearCervezaAsync(nuevaCerveza);

                if (exito)
                {
                    await DisplayAlert("¡Éxito!", "La cerveza se ha guardado en la base de datos.", "OK");
                    await Navigation.PopAsync();
                } // <--- ESTA ES LA LLAVE QUE FALTABA
                else
                {
                    await DisplayAlert("Error 400", "Bad Request: La API rechazó los datos.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error de Conexión", $"Fallo al intentar guardar: {ex.Message}", "OK");
            }
        }
    }
}