using Microsoft.AspNetCore.Mvc;
using VehiculosAPI.DTOs;
using VehiculosAPI.Services;

namespace VehiculosAPI.Controllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // Apagado temporalmente para el CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly ICatMarcaService catMarcaService;

        public CatalogosController(ICatMarcaService catMarcaService)
        {
            this.catMarcaService = catMarcaService;
        }

        [HttpGet("marcas")]
        public async Task<IActionResult> ObtenerMarcas()
        {
            var marcas = await catMarcaService.GetAsync();
            return Ok(marcas);
        }

        [HttpPost("nuevamarca")]
        public async Task<IActionResult> CrearMarca([FromBody] CrearCatMarcaDTO crearMarca)
        {
            try
            {
                int nuevaMarcaId = await catMarcaService.CreateAsync(crearMarca);
                return Ok(new { Id = nuevaMarcaId, Marca = crearMarca.Marca });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        
        [HttpPut("marcas/{id}")]
        public async Task<IActionResult> ActualizarMarca(int id, [FromBody] CrearCatMarcaDTO dto)
        {
            await catMarcaService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("marcas/{id}")]
        public async Task<IActionResult> EliminarMarca(int id)
        {
            await catMarcaService.DeleteAsync(id);
            return NoContent();
        }
    }
}