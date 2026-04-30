using VehiculosAPI.DTOs.AuthDTOs;

namespace VehiculosAPI.Services.AuthServices
{
    public interface IAuthService
    {
        Task<bool> CreateUser(NewUserDTO newUser);
        Task<bool> Login(UserDTO userDTO);
    }
}
