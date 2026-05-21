using VehiculosMAUI.DTOs;

namespace VehiculosMAUI.Services.HttpServices;

public interface IApiService
{
    Task<List<CervezaDTO>> ObtenerCervezasAsync();
    Task<CervezaDTO> ObtenerCervezaPorIdAsync(int id);
    Task<bool> CrearCervezaAsync(CervezaDTO dto);
    Task<bool> ActualizarCervezaAsync(int id, CervezaDTO dto);
    Task<bool> EliminarCervezaAsync(int id);
    Task<T> PostAsync<T>(string endpoint, object data);

}