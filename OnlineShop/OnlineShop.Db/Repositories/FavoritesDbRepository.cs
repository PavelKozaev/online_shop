using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;

namespace OnlineShop.Db.Repositories
{
    public class FavoritesDbRepository : IFavoritesRepository
    {
        private readonly DatabaseContext dbContext;

        public FavoritesDbRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Favorites> GetAll(string userId)
        {
            return dbContext.Favorites.Where(f => f.UserId == userId).Include(f => f.Product).ToList();
        }

        public void Add(Product? product, string userId)
        {
            var existingFavorites = dbContext.Favorites.Where(f => f.UserId == userId).Include(f => f.Product).ToList();

            if (!existingFavorites.Any(f => f.Product == product))
            {
                var favorites = new Favorites()
                {
                    Product = product,
                    UserId = userId
                };
                dbContext.Favorites.Add(favorites);
                dbContext.SaveChanges();
            }
        }

        public void Remove(Product? product, string userId)
        {
            var existingFavorites = dbContext.Favorites.Where(f => f.UserId == userId).Include(f => f.Product).ToList();

            if (existingFavorites.Any(f => f.Product == product))
            {
                var FavoritesItemToRemove = existingFavorites.FirstOrDefault(f => f.Product == product);
                dbContext.Favorites.Remove(FavoritesItemToRemove);
                dbContext.SaveChanges();
            }
        }
    }
}