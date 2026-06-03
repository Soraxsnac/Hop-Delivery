using HopDelivery.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

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