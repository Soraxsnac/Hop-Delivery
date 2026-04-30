using Microsoft.AspNetCore.Mvc;
using VehiculosAPI.DTOs.AuthDTOs;
using VehiculosAPI.Services.AuthServices;

namespace VehiculosAPI.Controllers.AuthControllers
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
            var result = await authService.CreateUser(newUser);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var result = await authService.Login(userDTO);
            return Ok(result);
        }
    }
}
