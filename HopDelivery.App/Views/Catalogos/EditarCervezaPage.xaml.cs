using System;
using Microsoft.Maui.Controls;
using HopDelivery.DTOs;
using HopDelivery.Services.HttpServices;

namespace HopDelivery.Views.Catalogos
{
    public partial class EditarCervezaPage : ContentPage
    {
        private readonly IApiService _apiService;
        private CervezaDTO _cervezaActual;

        public EditarCervezaPage(CervezaDTO cerveza)
        {
            InitializeComponent();
            _apiService = IPlatformApplication.Current.Services.GetService<IApiService>();

            _cervezaActual = cerveza;
            CargarDatosEnPantalla();
        }

        private void CargarDatosEnPantalla()
        {
            LblId.Text = $"ID Interno: {_cervezaActual.Id}";
            TxtNombre.Text = _cervezaActual.Nombre;
            TxtTipo.Text = _cervezaActual.Tipo;
            TxtABV.Text = _cervezaActual.ABV.ToString();
            TxtIBU.Text = _cervezaActual.IBU.ToString();
            TxtDescripcion.Text = _cervezaActual.Descripcion;
            TxtImagenUrl.Text = _cervezaActual.ImagenURL;
            PickerCalificacion.SelectedIndex = _cervezaActual.Calificacion;
        }

        private async void OnActualizarClicked(object sender, EventArgs e)
        {
            _cervezaActual.Nombre = TxtNombre.Text.Trim();
            _cervezaActual.Tipo = TxtTipo.Text?.Trim() ?? "General";
            _cervezaActual.ABV = double.TryParse(TxtABV.Text, out var abv) ? abv : 0.0;
            _cervezaActual.IBU = int.TryParse(TxtIBU.Text, out var ibu) ? ibu : 0;
            _cervezaActual.Descripcion = TxtDescripcion.Text?.Trim() ?? "";
            _cervezaActual.ImagenURL = TxtImagenUrl.Text?.Trim() ?? "";
            _cervezaActual.Calificacion = PickerCalificacion.SelectedIndex >= 0 ? PickerCalificacion.SelectedIndex : 0;
            _cervezaActual.IdMarca = 1;

            try
            {
                // Enviamos tanto el ID como el objeto DTO completo que requiere tu interfaz de servicio
                var exito = await _apiService.ActualizarCervezaAsync(_cervezaActual.Id, _cervezaActual);

                if (exito)
                {
                    await DisplayAlert("Éxito", "Cerveza actualizada correctamente", "OK");
                    await Navigation.PopAsync(); // Regresa al catálogo principal
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo actualizar en la base de datos.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Problema de conexión: {ex.Message}", "OK");
            }
        }
    }
}