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

        public async Task<TokenDTO> CreateUser(NewUserDTO newUser)
        {
            var newIdentityUser = new IdentityUser
            {
                UserName = newUser.Email,
                Email = newUser.Email
            };

            var result = await userManager.CreateAsync(newIdentityUser, newUser.Password);

            if (result.Succeeded)
            {
                return BuildToken(newUser.Email);
            }

            return new TokenDTO();
        }

        public async Task<TokenDTO> Login(UserDTO userDTO)
        {
            var result = await signInManager.PasswordSignInAsync
                (userDTO.Email, userDTO.Password, false, false);

            if (result.Succeeded)
            {
                return BuildToken(userDTO.Email);
            }

            return new TokenDTO();
        }

        private TokenDTO BuildToken(string email)
        {
            var claims = new List<Claim>()
            {
                new Claim("User", email),
                new Claim("Validado", "true")
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