using Shop.Helpers;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Repositories
{
    public class OrderRepository
    {
        private List<Order> orders;
        private int idCounter = 1;

        public OrderRepository()
        {
            orders = FileService.LoadOrders();
            if (orders.Any())
                idCounter = orders.Max(o => o.Id) + 1;
        }

        public void AddOrder(Order order)
        {
            order.Id = idCounter++;
            orders.Add(order);
            FileService.SaveOrders(orders);
        }

        public IEnumerable<Order> GetAllOrders() => orders;
    }
}