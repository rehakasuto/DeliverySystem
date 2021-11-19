using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverySystem.API.Models
{
    /// <summary>
    /// The partner who deliver her/his product to customer
    /// </summary>
    public class Partner : Base, IAccount
    {
        /// <summary>
        /// Name of the partner
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Extra Info of the partner
        /// </summary>
        [Required, MaxLength(500)]
        public string Description { get; set; }
        /// <summary>
        /// Delivered orders of the partner
        /// </summary>
        [InverseProperty("Partner")]
        public virtual ICollection<Order> Orders { get; set; }
        /// <summary>
        /// Email info of the partner
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password of the partner
        /// </summary>
        public string Password { get; set; }
    }
}
