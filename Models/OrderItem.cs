using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverySystem.API.Models
{
    /// <summary>
    /// Item of the order
    /// </summary>
    public class OrderItem : Base
    {
        /// <summary>
        /// Order identifier
        /// </summary>
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        /// <summary>
        /// Order object
        /// </summary>
        public virtual Order Order { get; set; }
        /// <summary>
        /// Product identifier of the order item
        /// </summary>
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        /// <summary>
        /// Product object of the order item
        /// </summary>
        public virtual Product Product { get; set; }
        /// <summary>
        /// Quantity of the product order
        /// </summary>
        public int Quantity { get; set; }
    }
}
