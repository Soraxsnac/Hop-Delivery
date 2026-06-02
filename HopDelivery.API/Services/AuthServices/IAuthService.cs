using HopDelivery.API.DTOs.AuthDTOs;

namespace HopDelivery.API.Services.AuthServices
{
    public interface IAuthService
    {
        Task<bool> CreateUser(NewUserDTO newUser);
        Task<TokenDTO> Login(UserDTO userDTO);
    }
}
