using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartRepository;
        public CartViewComponent(ICartsRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public IViewComponentResult Invoke()
        {
            var cart = cartRepository.TryGetByUserId(Constants.UserId);

            var cartViewModel = Mapping.ToCartViewModel(cart);
                        
            var productCounts = cartViewModel?.Amount ?? 0;

            return View("Cart", productCounts);
        }        
    }
}
