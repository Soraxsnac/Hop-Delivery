using Microsoft.EntityFrameworkCore;
using VehiculosAPI.Data;
using VehiculosAPI.DTOs;
using VehiculosAPI.Entities.Catalogos;

namespace VehiculosAPI.Services
{
    public class CatMarcaService : ICatMarcaService
    {
        private readonly ApplicationDbContext _context;

        public CatMarcaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MarcaDTO>> GetAsync()
        {
            return await _context.CatMarcas
                .Select(m => new MarcaDTO { Id = m.Id, Marca = m.Marca })
                .ToListAsync();
        }

        public async Task<int> CreateAsync(CrearCatMarcaDTO dto)
        {
            var marca = new CatMarca { Marca = dto.Marca };
            _context.CatMarcas.Add(marca);
            await _context.SaveChangesAsync();
            return marca.Id;
        }

        public async Task UpdateAsync(int id, CrearCatMarcaDTO dto)
        {
            var marca = await _context.CatMarcas.FindAsync(id);
            if (marca != null)
            {
                marca.Marca = dto.Marca;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var marca = await _context.CatMarcas.FindAsync(id);
            if (marca != null)
            {
                _context.CatMarcas.Remove(marca);
                await _context.SaveChangesAsync();
            }
        }
    }
}