using System.ComponentModel.DataAnnotations;

namespace HopDelivery.API.DTOs.AuthDTOs
{
    public class UserDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}