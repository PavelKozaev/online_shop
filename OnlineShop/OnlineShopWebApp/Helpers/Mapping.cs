using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static List<ProductViewModel> ToProductViewModels(List<Product> products)
        {
            var productsViewModel = new List<ProductViewModel>();

            foreach (var product in products)
            {                
                productsViewModel.Add(ToProductViewModel(product));
            }

            return productsViewModel;
        }


        public static ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Author = product.Author,
                Cost = product.Cost,
                Description = product.Description,
                ImagePath = product.ImagePath
            };
        }


        public static CartViewModel ToCartViewModel(Cart cart)
        {
            if (cart == null)
            {
                return null;
            }

            return new CartViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = ToCartItemViewModels(cart.Items)
            };
        }


        public static List<CartItemViewModel> ToCartItemViewModels(List<CartItem> cartDbItems)
        {
            var cartItems = new List<CartItemViewModel>();

            foreach (var cartDbItem in cartDbItems)
            {
                var cartItem = new CartItemViewModel
                {
                    Id = cartDbItem.Id,
                    Amount = cartDbItem.Amount,
                    Product = ToProductViewModel(cartDbItem.Product)
                };

                cartItems.Add(cartItem);
            }

            return cartItems;
        }
    }
}
