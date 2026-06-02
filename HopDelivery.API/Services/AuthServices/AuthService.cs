using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HopDelivery.API.DTOs.AuthDTOs;

namespace HopDelivery.API.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public async Task<bool> CreateUser(NewUserDTO newUser)
        {
            var newIdentityUser = new IdentityUser
            {
                UserName = newUser.userName,
                Email = newUser.email
            };
            var result = await userManager.CreateAsync(newIdentityUser, newUser.password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<TokenDTO> Login(UserDTO userDTO)
        {
            var result = await signInManager.PasswordSignInAsync
                (userDTO.UserName, userDTO.Password, false, false);

            if (result.Succeeded)
            {
                return BuildToken(userDTO.UserName);
            }
            return new TokenDTO();
        }   

        private TokenDTO BuildToken(string userName)
        {
            var claims = new List<Claim>()
            {
            // ¡OJO! No enviar datos sensibles, solamente son de prueba
             new Claim("User", userName),
             new Claim("ColorFavorito", "Azul"),
             new Claim("Validado", "true"),
             new Claim("NumeroTarjeta", "123456"),
             new Claim("ExpiracionTarjeta", "12/25"),
             new Claim("CVVTarjeta", "123")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtkey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var expiration = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new TokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
