using DeliverySystem.API.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeliverySystem.API.Models
{
    /// <summary>
    /// This model store the action of delivery states
    /// </summary>
    public class DeliveryAction : Base
    {
        /// <summary>
        /// Identifier of the delivery
        /// </summary>
        [Required]
        public Guid DeliveryId { get; set; }
        /// <summary>
        /// Identifier of the Partner
        /// </summary>
        public Guid? PartnerId { get; set; }
        /// <summary>
        /// Identifier of the User
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Status of the delivery
        /// </summary>
        public Enums.DeliveryState DeliveryState { get; set; }
    }
}
