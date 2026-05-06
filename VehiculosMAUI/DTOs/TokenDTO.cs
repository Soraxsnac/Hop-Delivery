using System;

namespace VehiculosMAUI.DTOs
{
    public class TokenDTO
    {
        public string token { get; set; }

        public DateTimeOffset expiration { get; set; }
    }
}
