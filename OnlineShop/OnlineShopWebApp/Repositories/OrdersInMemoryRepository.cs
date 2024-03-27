using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class OrdersInMemoryRepository : IOrdersRepository
    {
        private List<Cart> orders = new();
        public void Add(Cart cart)
        {
            orders.Add(cart);
        }
    }
}
