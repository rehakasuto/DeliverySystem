namespace DeliverySystem.API.Models.ResponseModels
{
    public class DeliveryResponse
    {
        public string state { get; set; }
        public AccessWindowResponse accessWindow { get; set; }
        public RecipientResponse recipient { get; set; }
        public OrderResponse order { get; set; }
    }

    public class AccessWindowResponse
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
    }

    public class RecipientResponse
    {
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }

    public class OrderResponse
    {
        public string orderNumber { get; set; }
        public string sender { get; set; }
    }
}
