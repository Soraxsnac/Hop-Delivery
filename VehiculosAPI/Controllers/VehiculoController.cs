
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehiculosAPI.Entities;
using VehiculosAPI.Entities.Catalogos;
using VehiculosAPI.Services;

namespace VehiculosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService vehiculoService;

        public VehiculoController(IVehiculoService vehiculoService)
        {
            this.vehiculoService = vehiculoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vehiculo>>> GetAllVehiculos()
        {
            var vehiculos = await vehiculoService.GetAllVehiculosAsync();
            //var vehiculos = new List<Vehiculo>();

            if (vehiculos.Count == 0)
            {
                return NoContent();
            }

            return Ok(vehiculos);
        }

        [HttpPost("nuevamarca")]
        public async Task<ActionResult<CatMarca>> RegistrarMarca([FromBody] CatMarca marca)
        {
            var nuevaMarca = await vehiculoService.RegistrarMarcaAsync(marca);
            if (nuevaMarca == null)
            {
                return BadRequest("No se pudo registrar la marca.");
            }
            return Ok(nuevaMarca);
        }

        [HttpPost("nuevovehiculo")]
        public async Task<ActionResult<Vehiculo>> RegistrarVehiculo([FromBody] Vehiculo vehiculo)
        {
            var nuevoVehiculo = await vehiculoService.RegistrarVehiculoAsync(vehiculo);
            if (nuevoVehiculo == null)
            {
                return BadRequest("No se pudo registrar el vehículo.");
            }
            return Ok(nuevoVehiculo);
        }

        [HttpGet("fromdb")]
        public async Task<ActionResult<List<Vehiculo>>> GetAllVehiculosFromDB()
        {
            var vehiculos = await vehiculoService.GetAllVehiculosFromDBAsync();
            if (vehiculos.Count == 0)
            {
                return NoContent();
            }
            return Ok(vehiculos);
        }

        [HttpPut("actualizarvehiculo")]
        public async Task<ActionResult<Vehiculo>> ActualizarVehiculo([FromBody] Vehiculo vehiculo)
        {
            var vehiculoActualizado = await vehiculoService.ActualizarVehiculoAsync(vehiculo);
            if (vehiculoActualizado == null)
            {
                return BadRequest("No se pudo actualizar el vehículo.");
            }
            return Ok(vehiculoActualizado);
        }


        [HttpDelete("eliminarvehiculo")]
        public async Task<ActionResult> EliminarVehiculo([FromBody] Vehiculo vehiculo)
        {
            var resultadoEliminacion = await vehiculoService.EliminarVehiculoAsync(vehiculo);
            if (!resultadoEliminacion)
            {
                return BadRequest("No se pudo eliminar el vehículo.");
            }
            return Ok("Vehículo eliminado exitosamente.");
        }
    }
}

