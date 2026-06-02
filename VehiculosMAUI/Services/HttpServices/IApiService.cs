using VehiculosMAUI.DTOs;

namespace VehiculosMAUI.Services.HttpServices;

public interface IApiService
{
    // --- METODOS PARA CERVEZAS ---
    Task<List<CervezaDTO>> ObtenerCervezasAsync();
    Task<CervezaDTO> ObtenerCervezaPorIdAsync(int id);
    Task<bool> CrearCervezaAsync(CervezaDTO dto);
    Task<bool> ActualizarCervezaAsync(int id, CervezaDTO dto);
    Task<bool> EliminarCervezaAsync(int id);

    Task<T> PostAsync<T>(string endpoint, object data);

  
    Task<List<MarcaDTO>> ObtenerMarcasAsync();
    Task<bool> CrearMarcaAsync(CrearCatMarcaDTO dto);
    Task<bool> ActualizarMarcaAsync(int id, CrearCatMarcaDTO dto);
    Task<bool> EliminarMarcaAsync(int id);
}