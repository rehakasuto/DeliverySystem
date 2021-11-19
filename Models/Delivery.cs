using DeliverySystem.API.Models.ResponseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static DeliverySystem.API.Helpers.Enums;

namespace DeliverySystem.API.Models
{
    /// <summary>
    /// Delivery object
    /// </summary>
    public class Delivery : Base
    {
        /// <summary>
        /// Order identifier of the delivery
        /// </summary>
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        /// <summary>
        /// Order instance of the delivery
        /// </summary>
        public virtual Order Order { get; set; }
        /// <summary>
        /// Beggining of the delivery
        /// </summary>
        public DateTimeOffset StartTime { get; set; }
        /// <summary>
        /// Estimated delivery date
        /// </summary>
        public DateTimeOffset EndTime { get; set; }
        /// <summary>
        /// Status of the delivery
        /// </summary>
        public DeliveryState State { get; set; }
    }
}
