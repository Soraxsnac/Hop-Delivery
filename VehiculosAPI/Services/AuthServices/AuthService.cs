using Microsoft.AspNetCore.Identity;
using VehiculosAPI.DTOs.AuthDTOs;

namespace VehiculosAPI.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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

        public async Task<bool> Login(UserDTO userDTO)
        {
            var result = await signInManager.PasswordSignInAsync
                (userDTO.UserName, userDTO.Password, false, false);

            if (result.Succeeded)
            {
                return true;
            } 
            return false;
        }   
    }
}
