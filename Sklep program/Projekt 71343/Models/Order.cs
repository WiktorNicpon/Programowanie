using System.Collections.Generic;

namespace Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public List<ProductOrder> Products { get; set; } = new();
    }

    public enum OrderStatus
    {
        Zlozone,
        Zrealizowane
    }
}