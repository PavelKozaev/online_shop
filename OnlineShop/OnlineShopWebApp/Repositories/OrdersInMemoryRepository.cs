using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Repositories
{
    public class OrdersInMemoryRepository : IOrdersRepository
    {
        private List<Order> orders = new();
        public void Add(Order order)
        {
            orders.Add(order);
        }
    }
}
