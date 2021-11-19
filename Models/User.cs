using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverySystem.API.Models
{
    /// <summary>
    /// The user who is the recipient
    /// </summary>
    public class User : Base, IAccount
    {
        /// <summary>
        /// First name of the recipient
        /// </summary>
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        /// <summary>
        /// Surname of the recipient
        /// </summary>
        [Required, MaxLength(100)]
        public string LastName { get; set; }
        /// <summary>
        /// Mobile number of the recipient
        /// </summary>
        [Required, MaxLength(100)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Delivery target location address of the recipient
        /// </summary>
        [Required, MaxLength(500)]
        public string Address { get; set; }
        /// <summary>
        /// Delivered orders of the recipient
        /// </summary>
        [InverseProperty("User")]
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
