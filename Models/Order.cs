namespace CNOrderApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; } 
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryExpected { get; set; }
        public Boolean ContainsGift { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } 

    }

}
