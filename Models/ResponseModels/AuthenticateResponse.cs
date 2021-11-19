using DeliverySystem.API.Helpers;

namespace DeliverySystem.API.Models.ResponseModels
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public Enums.AccountType AccountType { get; set; }
    }
}
