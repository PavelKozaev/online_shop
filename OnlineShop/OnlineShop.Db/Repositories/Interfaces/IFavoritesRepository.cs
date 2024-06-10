using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        Task<Favorites> TryGetByUserNameAsync(string userName);
        Task AddAsync(Product product, string userName);
        Task RemoveAsync(Product product, string userName);
        Task ClearAsync(string userName);
    }
}
