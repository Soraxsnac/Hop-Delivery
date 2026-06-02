namespace HopDelivery.API.DTOs.AuthDTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
