using VehiculosAPI.DTOs.AuthDTOs;

namespace VehiculosAPI.Services.AuthServices
{
    public interface IAuthService
    {
        Task<bool> CreateUser(NewUserDTO newUser);
        Task<TokenDTO> Login(UserDTO userDTO);
    }
}
