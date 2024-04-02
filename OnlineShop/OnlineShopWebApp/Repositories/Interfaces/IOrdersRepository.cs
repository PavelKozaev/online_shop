using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        void Add(Order order);
    }
}