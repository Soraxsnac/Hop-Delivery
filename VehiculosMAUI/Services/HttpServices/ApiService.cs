using System.Text;
using System.Text.Json;
using VehiculosMAUI.Services.HttpServices;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        await GetTokenAsync();
        var response = await _httpClient.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<T> GetByIdAsync<T>(string endpoint, int id)
    {
        await GetTokenAsync();
        var response = await _httpClient.GetAsync($"{endpoint}/{id}");

        if (!response.IsSuccessStatusCode)
            return default;

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, _options);
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        await GetTokenAsync();
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
            return default;

        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(result, _options);
    }

    public async Task<T> PutAsync<T>(string endpoint, int id, object data)
    {
        await GetTokenAsync();
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{endpoint}/{id}", content);

        if (!response.IsSuccessStatusCode)
            return default;

        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(result, _options);
    }

    public async Task<bool> DeleteAsync(string endpoint, int id)
    {
        await GetTokenAsync();
        var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
        return response.IsSuccessStatusCode;
    }

   private async Task GetTokenAsync()
    {
        var token = await SecureStorage.GetAsync("TokenApp");

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
}