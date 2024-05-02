using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        List<Favorites> GetAll(string userId);
        void Add(Product? product, string userId);
        void Remove(Product? product, string userId);
    }
}
