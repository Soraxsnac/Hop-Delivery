using System;

namespace HopDelivery.App.DTOs
{
    public class TokenDTO
    {
        public string token { get; set; }

        public DateTimeOffset expiration { get; set; }
    }
}
