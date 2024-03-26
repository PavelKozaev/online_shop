using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public interface ICartsRepository
    {
        void Add(Product product, string userId);
        Cart TryGetByUserId(string userId);
    }
}