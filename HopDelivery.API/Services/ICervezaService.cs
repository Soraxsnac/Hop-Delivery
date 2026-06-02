using HopDelivery.API.DTOs;

namespace HopDelivery.API.Services
{
    public interface ICervezaService
    {
        Task<List<CervezaDTO>> ObtenerTodasAsync();
        Task<CervezaDTO> ObtenerPorIdAsync(int id);
        Task<CervezaDTO> CrearAsync(CrearCervezaDTO dto);
        Task ActualizarAsync(int id, CrearCervezaDTO dto);
        Task EliminarAsync(int id);
    }
}