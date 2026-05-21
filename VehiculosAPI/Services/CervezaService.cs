using Microsoft.EntityFrameworkCore;
using VehiculosAPI.Data;
using VehiculosAPI.DTOs;
using VehiculosAPI.Entities;

namespace VehiculosAPI.Services
{
    public class CervezaService : ICervezaService
    {
        private readonly ApplicationDbContext _context;

        public CervezaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CervezaDTO>> ObtenerTodasAsync()
        {
            return await _context.Cervezas
                .Select(c => new CervezaDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Tipo = c.Tipo,
                    IBU = c.IBU,
                    ABV = c.ABV,
                    Descripcion = c.Descripcion,
                    ImagenURL = c.ImagenURL
                }).ToListAsync();
        }

        public async Task<CervezaDTO> ObtenerPorIdAsync(int id)
        {
            var c = await _context.Cervezas.FindAsync(id);
            if (c == null) return null;

            return new CervezaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Tipo = c.Tipo,
                IBU = c.IBU,
                ABV = c.ABV,
                Descripcion = c.Descripcion,
                ImagenURL = c.ImagenURL
            };
        }

        public async Task<CervezaDTO> CrearAsync(CrearCervezaDTO dto)
        {
            var cerveza = new Cerveza
            {
                Nombre = dto.Nombre,
                Tipo = dto.Tipo,
                IBU = dto.IBU,
                ABV = dto.ABV,
                Descripcion = dto.Descripcion,
                ImagenURL = dto.ImagenURL
            };

            _context.Cervezas.Add(cerveza);
            await _context.SaveChangesAsync();

            return await ObtenerPorIdAsync(cerveza.Id);
        }

        public async Task ActualizarAsync(int id, CrearCervezaDTO dto)
        {
            var cerveza = await _context.Cervezas.FindAsync(id);
            if (cerveza != null)
            {
                cerveza.Nombre = dto.Nombre;
                cerveza.Tipo = dto.Tipo;
                cerveza.IBU = dto.IBU;
                cerveza.ABV = dto.ABV;
                cerveza.Descripcion = dto.Descripcion;
                cerveza.ImagenURL = dto.ImagenURL;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarAsync(int id)
        {
            var cerveza = await _context.Cervezas.FindAsync(id);
            if (cerveza != null)
            {
                _context.Cervezas.Remove(cerveza);
                await _context.SaveChangesAsync();
            }
        }
    }
}