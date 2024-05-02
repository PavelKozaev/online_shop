using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        Favorites TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void Remove(Product product, string userId);   
        void Clear(string userId);
    }
}
