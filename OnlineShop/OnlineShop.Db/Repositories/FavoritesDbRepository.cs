using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;

namespace OnlineShop.Db.Repositories
{
    public class FavoritesDbRepository : IFavoritesRepository
    {
        private readonly DatabaseContext dbContext;
        private readonly IdentityContext identityContext;


        public FavoritesDbRepository(DatabaseContext dbContext, IdentityContext identityContext)
        {
            this.dbContext = dbContext;
            this.identityContext = identityContext;
        }


        public Favorites TryGetByUserName(string userName)
        {
            return dbContext.Favorites
                .Include(f => f.Products)
                .Include(u => u.User)
                .FirstOrDefault(f => f.User.UserName == userName);
        }


        public void Add(Product product, string userName)
        {
            var user = identityContext.Users.FirstOrDefault(u => u.UserName == userName);

            var favorites = dbContext.Favorites.Include(f => f.User)
                                               .FirstOrDefault(f => f.User.Id == user.Id);

            if (favorites is null)
            {
                dbContext.Favorites.Add(new Favorites()
                {
                    User = user,
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