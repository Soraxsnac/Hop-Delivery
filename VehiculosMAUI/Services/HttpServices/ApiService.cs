using System.Net.Http.Json;
using VehiculosMAUI.DTOs;

namespace VehiculosMAUI.Services.HttpServices;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        // 1. Límite de 5 segundos. Si no encuentra el servidor rápido, falla y no congela la app.
        _httpClient.Timeout = TimeSpan.FromSeconds(5);

        // 2. Detección de plataforma
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            // El emulador Android SIEMPRE debe usar 10.0.2.2
            _baseUrl = "http://10.0.2.2:5032/api/Cervezas";
        }
        else
        {
            // Windows usa la IP configurada en el launchSettings.json de la API
            _baseUrl = "http://127.0.0.1:5032/api/Cervezas";
        }
    }

    public async Task<List<CervezaDTO>> ObtenerCervezasAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CervezaDTO>>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error HTTP GET: {ex.Message}");
        }
        return new List<CervezaDTO>();
    }

    public async Task<CervezaDTO> ObtenerCervezaPorIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CervezaDTO>();
            }
        }
        catch { }
        return null;
    }

    public async Task<bool> CrearCervezaAsync(CervezaDTO dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, dto);

            if (!response.IsSuccessStatusCode)
            {
                // Extraemos el error de la API si la base de datos lo rechaza
                string errorDetalle = await response.Content.ReadAsStringAsync();
                throw new Exception($"Estado: {response.StatusCode}\nDetalle de la API: {errorDetalle}");
            }

            return true;
        }
        catch (Exception)
        {
            // Lanzamos el error hacia arriba para que la interfaz lo muestre en pantalla
            throw;
        }
    }

    public async Task<bool> ActualizarCervezaAsync(int id, CervezaDTO dto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    public async Task<bool> EliminarCervezaAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        try
        {
            var authUrl = _baseUrl.Replace("Cervezas", endpoint);
            var response = await _httpClient.PostAsJsonAsync(authUrl, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error de Auth: {ex.Message}");
        }
        return default;
    }
}