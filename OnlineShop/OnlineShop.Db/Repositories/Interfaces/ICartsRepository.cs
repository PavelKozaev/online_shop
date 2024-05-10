using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface ICartsRepository
    {
        void Add(Product product, string userName);
        void Clear(string userId);
        void DecreaseAmount(Guid productId, string userName);
        Cart TryGetByUserId(string userName);
    }
}