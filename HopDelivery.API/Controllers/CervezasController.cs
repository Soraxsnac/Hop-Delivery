using Microsoft.AspNetCore.Mvc;
using HopDelivery.API.DTOs;
using HopDelivery.API.Services;

namespace HopDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CervezasController : ControllerBase
    {
        private readonly ICervezaService _service;

        public CervezasController(ICervezaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CervezaDTO>>> Get()
        {
            return await _service.ObtenerTodasAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CervezaDTO>> Get(int id)
        {
            var cerveza = await _service.ObtenerPorIdAsync(id);
            if (cerveza == null) return NotFound();
            return cerveza;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CrearCervezaDTO dto)
        {
            var nuevaCerveza = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = nuevaCerveza.Id }, nuevaCerveza);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CrearCervezaDTO dto)
        {
            await _service.ActualizarAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.EliminarAsync(id);
            return NoContent();
        }
    }
}