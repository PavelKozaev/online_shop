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


        public Favorites TryGetByUserName(string userName)
        {
            return dbContext.Favorites
                .Include(f => f.Products)
                .FirstOrDefault(f => f.UserName == userName);
        }


        public void Add(Product product, string userName)
        {
            var favorites = TryGetByUserName(userName);

            if (favorites is null)
            {
                dbContext.Favorites.Add(new Favorites()
                {
                    UserName = userName,
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


        public void Remove(Product product, string userName)
        {
            var favorites = TryGetByUserName(userName);
               
            favorites.Products.Remove(product);

            dbContext.SaveChanges();
        }

        public void Clear(string userName)
        {
            var favorites = TryGetByUserName(userName);
            favorites.Products.Clear();
            dbContext.SaveChanges();
        }
    }
}