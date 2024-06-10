using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface ICartsRepository
    {
        Task AddAsync(Product product, string userName);
        Task ClearAsync(string userId);
        Task DecreaseAmountAsync(Guid productId, string userName);
        Task<Cart> TryGetByUserNameAsync(string userName);
    }
}