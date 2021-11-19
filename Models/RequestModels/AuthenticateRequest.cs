﻿using System.ComponentModel.DataAnnotations;

namespace DeliverySystem.API.Models.RequestModels
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
