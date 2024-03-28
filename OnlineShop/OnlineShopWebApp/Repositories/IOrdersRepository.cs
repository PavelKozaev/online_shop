using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public interface IOrdersRepository
    {
        void Add(Order order);
    }
}