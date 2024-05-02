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


        public Favorites TryGetByUserId(string userId)
        {
            return dbContext.Favorites
                .Include(f => f.Products)
                .FirstOrDefault(f => f.UserId == userId);
        }


        public void Add(Product product, string userId)
        {
            var favorites = TryGetByUserId(userId);

            if (favorites is null)
            {
                dbContext.Favorites.Add(new Favorites()
                {
                    UserId = userId,
                    Products = new List<Product>()
                    {
                        product
                    }
                });
            }
            else
            {
                favorites.Products.Add(product);
            }                
                        
            dbContext.SaveChanges();
        }        


        public void Remove(Product product, string userId)
        {
            var favorites = TryGetByUserId(userId);
               
            favorites.Products.Remove(product);

            dbContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var favorites = TryGetByUserId(userId);
            favorites.Products.Clear();
            dbContext.SaveChanges();
        }
    }
}