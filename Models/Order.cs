using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverySystem.API.Models
{
    /// <summary>
    /// Order object
    /// </summary>
    public class Order : Base
    {
        /// <summary>
        /// Supplier identifier of the order 
        /// </summary>
        [ForeignKey("Partner")]
        public Guid PartnerId { get; set; }
        /// <summary>
        /// Supplier object of the order 
        /// </summary>
        public virtual Partner Partner { get; set; }
        /// <summary>
        /// Customer identifier of the order 
        /// </summary>
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        /// <summary>
        /// Customer object of the order 
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Order Item collection
        /// </summary>
        [InverseProperty("Order")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
