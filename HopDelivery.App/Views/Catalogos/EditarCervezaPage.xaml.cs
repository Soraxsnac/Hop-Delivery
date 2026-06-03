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

            PickerTipo.ItemsSource = new string[] { "Lager", "IPA", "Stout / Porter" };
            PickerMarca.ItemsSource = new string[] { "Grupo Modelo", "Heineken México", "Guinness", "Artesanal / Otra" };
            PickerCalificacion.ItemsSource = new string[] { "0 Estrellas (Pésima)", "1 Estrella", "2 Estrellas", "3 Estrellas (Regular)", "4 Estrellas", "5 Estrellas (Excelente)" };

            _cervezaActual = cerveza;
            CargarDatosEnPantalla();
        }

        private void CargarDatosEnPantalla()
        {
            LblId.Text = $"ID Interno: {_cervezaActual.Id}";
            TxtNombre.Text = _cervezaActual.Nombre;
            TxtABV.Text = _cervezaActual.ABV.ToString();
            TxtIBU.Text = _cervezaActual.IBU.ToString();
            TxtDescripcion.Text = _cervezaActual.Descripcion;
            PickerCalificacion.SelectedIndex = _cervezaActual.Calificacion;
            PickerTipo.SelectedItem = _cervezaActual.Tipo;

            // Preseleccionamos la marca que tenía para que la función de abajo sepa qué imagen poner
            PickerMarca.SelectedIndex = (_cervezaActual.IdMarca >= 1 && _cervezaActual.IdMarca <= 4) ? (_cervezaActual.IdMarca - 1) : 3;
        }

        private string ObtenerUrlImagenSegura(int index)
        {
            if (index == 0) return "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Corona-6Pack.JPG/800px-Corona-6Pack.JPG";
            if (index == 1) return "https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Heineken_pils_bottle.png/400px-Heineken_pils_bottle.png";
            if (index == 2) return "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/Pint_of_Guinness_Mac.jpg/400px-Pint_of_Guinness_Mac.jpg";
            return "https://upload.wikimedia.org/wikipedia/commons/thumb/4/42/Beer_tasting.jpg/800px-Beer_tasting.jpg";
        }

        private async void OnActualizarClicked(object sender, EventArgs e)
        {
            _cervezaActual.Nombre = TxtNombre.Text.Trim();
            _cervezaActual.Tipo = PickerTipo.SelectedIndex >= 0 ? PickerTipo.SelectedItem.ToString() : "Lager";
            _cervezaActual.ABV = double.TryParse(TxtABV.Text, out var abv) ? abv : 0.0;
            _cervezaActual.IBU = int.TryParse(TxtIBU.Text, out var ibu) ? ibu : 0;
            _cervezaActual.Descripcion = TxtDescripcion.Text?.Trim() ?? "";

            // Asignamos la imagen en el momento exacto de guardar, calculando lo que esté en el Picker
            _cervezaActual.ImagenURL = ObtenerUrlImagenSegura(PickerMarca.SelectedIndex);

            _cervezaActual.IdMarca = PickerMarca.SelectedIndex >= 0 ? (PickerMarca.SelectedIndex + 1) : 4;
            _cervezaActual.Calificacion = PickerCalificacion.SelectedIndex >= 0 ? PickerCalificacion.SelectedIndex : 0;

            try
            {
                var exito = await _apiService.ActualizarCervezaAsync(_cervezaActual.Id, _cervezaActual);
                if (exito)
                {
                    await DisplayAlert("Éxito", "Cerveza actualizada", "OK");
                    await Navigation.PopAsync();
                }
                else await DisplayAlert("Error", "Fallo al actualizar.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Conexión: {ex.Message}", "OK");
            }
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            bool confirmar = await DisplayAlert("Advertencia", $"¿Estás seguro que deseas eliminar {_cervezaActual.Nombre} de forma permanente?", "Sí, Eliminar", "Cancelar");

            if (confirmar)
            {
                try
                {
                    var exito = await _apiService.EliminarCervezaAsync(_cervezaActual.Id);

                    if (exito)
                    {
                        await DisplayAlert("Eliminada", "La cerveza fue borrada de la base de datos.", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo eliminar en la API.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error de Conexión", $"Fallo al intentar borrar: {ex.Message}", "OK");
                }
            }
        }
    }
}