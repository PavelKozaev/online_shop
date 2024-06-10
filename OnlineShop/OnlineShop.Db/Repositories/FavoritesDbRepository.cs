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

        public async Task<Favorites> TryGetByUserNameAsync(string userName)
        {
            return await dbContext.Favorites
                .Include(f => f.Products)
                .Include(u => u.User)
                .FirstOrDefaultAsync(f => f.User.UserName == userName);
        }

        public async Task AddAsync(Product product, string userName)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            var favorites = await dbContext.Favorites
                .Include(f => f.User)
                .Include(f => f.Products)
                .FirstOrDefaultAsync(f => f.User.UserName == userName);

            if (favorites is null)
            {
                await dbContext.Favorites.AddAsync(new Favorites
                {
                    User = user,
                    Products = new List<Product> { product }
                });
            }
            else
            {
                favorites.Products.Add(product);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Product product, string userName)
        {
            var favorites = await TryGetByUserNameAsync(userName);

            favorites.Products.Remove(product);

            await dbContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userName)
        {
            var favorites = await TryGetByUserNameAsync(userName);
            favorites.Products.Clear();
            await dbContext.SaveChangesAsync();
        }
    }
}