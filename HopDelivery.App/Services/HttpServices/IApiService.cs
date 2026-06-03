using System.Collections.Generic;
using System.Threading.Tasks;
using HopDelivery.DTOs; // Para que reconozca tus DTOs

namespace HopDelivery.Services.HttpServices
{
    public interface IApiService
    {
   
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
}