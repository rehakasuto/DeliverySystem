using DeliverySystem.API.Helpers;
using System;

namespace DeliverySystem.API.Models.RequestModels
{
    public class DeliveryRequest
    {
        public Guid? DeliveryId { get; set; }
        public Guid OrderId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public Enums.DeliveryState? DeliveryState { get; set; }
    }
}
