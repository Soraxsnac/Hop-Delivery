using System;

namespace HopDelivery.App.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public DateTimeOffset Expiration { get; set; }
    }
}
