using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class CartsInMemoryRepository : ICartsRepository
    {
        private readonly List<Cart> carts = [];

        public Cart TryGetByUserId(string userId)
        {
            return carts.FirstOrDefault(c => c.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);

            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items =
                    [
                        new CartItem
                        {
                            Id = Guid.NewGuid() ,
                            Amount = 1,
                            Product = product
                        }
                    ]
                };

                carts.Add(newCart);
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
                        Id = Guid.NewGuid(),
                        Amount = 1,
                        Product = product
                    });
                }
            }
        }
    }
}
