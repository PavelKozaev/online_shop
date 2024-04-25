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


        public Cart TryGetByUserId(string userId)
        {
            return databaseContext.Carts
                .AsNoTracking()
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                .FirstOrDefault(c => c.UserId == userId);
        }


        public void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);

            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    UserId = userId
                };

                newCart.Items = new List<CartItem>
                {
                    new CartItem
                    {
                        Amount = 1, 
                        Product = product
                    }
                };

                databaseContext.Carts.Add(newCart);
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
                        Product = product
                    });
                }
            }

            databaseContext.SaveChanges();
        }


        public void DecreaseAmount(Guid productId, string userId)
        {
            var existingCart = TryGetByUserId(userId);

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

            databaseContext.SaveChanges();
        }


        public void Clear(string userId)
        {
            var existingCart = TryGetByUserId(userId);
                        
            databaseContext.Carts.Remove(existingCart);

            databaseContext.SaveChanges();                     
        }
    }
}
