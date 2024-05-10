using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        Favorites TryGetByUserName(string userName);
        void Add(Product product, string userName);
        void Remove(Product product, string userName);   
        void Clear(string userName);
    }
}
