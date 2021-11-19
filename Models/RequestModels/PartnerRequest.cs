using System.ComponentModel.DataAnnotations;

namespace DeliverySystem.API.Models.RequestModels
{
    public class PartnerRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
