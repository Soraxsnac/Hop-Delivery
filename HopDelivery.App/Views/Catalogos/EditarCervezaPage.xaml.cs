using HopDelivery.DTOs;
using HopDelivery.Services.HttpServices;

namespace HopDelivery.Views.Catalogos
{
    public partial class EditarCervezaPage : ContentPage
    {
        private readonly IApiService _apiService;
        private CervezaDTO _c;

        public EditarCervezaPage(CervezaDTO c)
        {
            InitializeComponent();
            _apiService = IPlatformApplication.Current.Services.GetService<IApiService>();
            _c = c;

            PickerTipo.ItemsSource = new string[] { "Lager", "IPA", "Stout / Porter" };
            PickerMarca.ItemsSource = new string[] { "Grupo Modelo", "Heineken México", "Guinness", "Artesanal / Otra" };
            PickerCalificacion.ItemsSource = new string[] { "0", "1", "2", "3", "4", "5" };

            TxtNombre.Text = _c.Nombre;
            TxtABV.Text = _c.ABV.ToString();
            PickerTipo.SelectedItem = _c.Tipo;
            PickerMarca.SelectedIndex = _c.IdMarca - 1;
        }

        private async void OnActualizarClicked(object sender, EventArgs e)
        {
            _c.Nombre = TxtNombre.Text;
            _c.Tipo = PickerTipo.SelectedItem.ToString();
            if (await _apiService.ActualizarCervezaAsync(_c.Id, _c)) await Navigation.PopAsync();
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Borrar", "¿Seguro?", "Sí", "No"))
            {
                if (await _apiService.EliminarCervezaAsync(_c.Id)) await Navigation.PopAsync();
            }
        }
    }
}