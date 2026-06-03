using Microsoft.AspNetCore.Mvc;
using HopDelivery.API.DTOs.AuthDTOs;
using HopDelivery.API.Services.AuthServices;

namespace HopDelivery.API.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] NewUserDTO newUser)
        {
            var tokenResult = await authService.CreateUser(newUser);

            if (tokenResult != null && !string.IsNullOrEmpty(tokenResult.Token))
            {
                return Ok(tokenResult);
            }
            return BadRequest(new { message = "No se pudo crear el usuario. Verifica la contraseña o si el correo ya existe." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var tokenResult = await authService.Login(userDTO);

            if (tokenResult != null && !string.IsNullOrEmpty(tokenResult.Token))
            {
                return Ok(tokenResult);
            }
            return Unauthorized(new { message = "Credenciales incorrectas." });
        }
    }
}