﻿namespace CNOrderApi.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order? Order { get; set; }
        public Customer? Customer { get; set; }

    }

}
