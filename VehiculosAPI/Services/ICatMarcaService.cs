using VehiculosAPI.DTOs;

namespace VehiculosAPI.Services
{
    public interface ICatMarcaService
    {
        Task<List<MarcaDTO>> GetAsync();
        Task<int> CreateAsync(CrearCatMarcaDTO dto);

        Task UpdateAsync(int id, CrearCatMarcaDTO dto);
        Task DeleteAsync(int id);
    }
}