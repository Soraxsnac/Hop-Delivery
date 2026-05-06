using VehiculosMAUI.DTOs;
using VehiculosMAUI.Services.HttpServices;

namespace VehiculosMAUI.Views;

public partial class RegistrarVehiculoPage : ContentPage
{
	private readonly IApiService apiService;

	public RegistrarVehiculoPage(IApiService apiService)
	{
		InitializeComponent();
		this.apiService = apiService;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		try
		{
			var marcas = await apiService.GetAsync<List<MarcaDTO>>("catalogos/marcas");

			comboMarca.ItemsSource = marcas;
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"No se pudieron cargar las marcas: {ex.Message}", "OK");
		}
	}

	private async void OnGuardarClicked(object sender, EventArgs e)
	{
		// Validar campos
		if (string.IsNullOrWhiteSpace(entryPlacas.Text))
		{
			await DisplayAlert("Validación", "Por favor ingrese las placas del vehículo", "OK");
			return;
		}

		if (comboMarca.SelectedItem == null)
		{
			await DisplayAlert("Validación", "Por favor seleccione una marca", "OK");
			return;
		}

		if (string.IsNullOrWhiteSpace(entryModelo.Text))
		{
			await DisplayAlert("Validación", "Por favor ingrese el modelo del vehículo", "OK");
			return;
		}

		if (numericAnio.Value == null || numericAnio.Value < 1900 || numericAnio.Value > 2030)
		{
			await DisplayAlert("Validación", "Por favor ingrese un año válido (1900-2030)", "OK");
			return;
		}

		try
		{
			// Deshabilitar botón mientras se guarda
			btnGuardar.IsEnabled = false;
			btnGuardar.Text = "Guardando...";

			// Aquí iría la lógica para guardar el vehículo
			// await apiService.PostAsync("vehiculos", nuevoVehiculo);

			// Simular guardado
			await Task.Delay(1000);

			await DisplayAlert("Éxito", "Vehículo registrado correctamente", "OK");

			// Limpiar formulario
			LimpiarFormulario();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"No se pudo guardar el vehículo: {ex.Message}", "OK");
		}
		finally
		{
			btnGuardar.IsEnabled = true;
			btnGuardar.Text = "Guardar";
		}
	}

	private async void OnCancelarClicked(object sender, EventArgs e)
	{
		bool confirmar = await DisplayAlert("Confirmar", 
			"¿Está seguro que desea cancelar? Los datos no guardados se perderán.", 
			"Sí", "No");

		if (confirmar)
		{
			LimpiarFormulario();
			await Navigation.PopAsync();
		}
	}

	private void LimpiarFormulario()
	{
		entryPlacas.Text = string.Empty;
		comboMarca.SelectedItem = null;
		entryModelo.Text = string.Empty;
		numericAnio.Value = 2024;
	}
}