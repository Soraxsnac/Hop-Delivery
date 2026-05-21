using System.Net.Http.Json;
using VehiculosMAUI.DTOs;

namespace VehiculosMAUI.Services.HttpServices;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    
    private readonly string _baseUrl = "http://localhost:5032/api/Cervezas";

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
        var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CervezaDTO>();
        }
        return null;
    }

    public async Task<bool> CrearCervezaAsync(CervezaDTO dto)
    {
        var response = await _httpClient.PostAsJsonAsync(_baseUrl, dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ActualizarCervezaAsync(int id, CervezaDTO dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> EliminarCervezaAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        return response.IsSuccessStatusCode;
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