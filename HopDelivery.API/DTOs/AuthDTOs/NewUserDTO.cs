using System.ComponentModel.DataAnnotations;

namespace HopDelivery.API.DTOs.AuthDTOs
{
    public class NewUserDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}