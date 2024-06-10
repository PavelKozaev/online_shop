using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;

namespace OnlineShop.Db.Repositories
{
    public class CartsDbRepository : ICartsRepository
    {
        private readonly DatabaseContext databaseContext;

        public CartsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Cart> TryGetByUserNameAsync(string userName)
        {
            return await databaseContext.Carts
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Include(x => x.User)
                .FirstOrDefaultAsync(c => c.User.UserName == userName);
        }

        public async Task AddAsync(Product product, string userName)
        {
            var user = await databaseContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            var existingCart = await TryGetByUserNameAsync(userName);

            if (existingCart is null)
            {
                var newCart = new Cart
                {
                    User = user
                };

                newCart.Items = new List<CartItem>
                {
                    new CartItem
                    {
                        Amount = 1,
                        Product = product,
                        Cart = newCart
                    }
                };

                await databaseContext.Carts.AddAsync(newCart);
            }
            else
            {
                var existingCartItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);

                if (existingCartItem != null)
                {
                    existingCartItem.Amount += 1;
                }
                else
                {
                    existingCart.Items.Add(new CartItem
                    {
                        Amount = 1,
                        Product = product,
                        Cart = existingCart
                    });
                }
            }

            await databaseContext.SaveChangesAsync();
        }

        public async Task DecreaseAmountAsync(Guid productId, string userName)
        {
            var existingCart = await TryGetByUserNameAsync(userName);
            var existingCartItem = existingCart?.Items?.FirstOrDefault(x => x.Product.Id == productId);

            if (existingCartItem == null)
            {
                return;
            }

            existingCartItem.Amount -= 1;

            if (existingCartItem.Amount == 0)
            {
                existingCart.Items.Remove(existingCartItem);
            }

            await databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userName)
        {
            var existingCart = await TryGetByUserNameAsync(userName);
            databaseContext.Carts.Remove(existingCart);
            await databaseContext.SaveChangesAsync();
        }
    }
}
