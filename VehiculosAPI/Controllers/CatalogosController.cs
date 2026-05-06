using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehiculosAPI.DTOs;
using VehiculosAPI.Services;

namespace VehiculosAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly ICatMarcaService catMarcaService;

        public CatalogosController(ICatMarcaService catMarcaService)
        {
            this.catMarcaService = catMarcaService;
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

        [HttpGet("marcas")]
        public async Task<IActionResult> ObtenerMarcas()
        {
            var marcas = await catMarcaService.GetAsync();
            return Ok(marcas);
        }
    }
}
