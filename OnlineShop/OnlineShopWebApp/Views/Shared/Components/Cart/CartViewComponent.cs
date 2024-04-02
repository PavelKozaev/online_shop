using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Repositories.Interfaces;

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

            var productCounts = cart?.Amount ?? 0;

            return View("Cart", productCounts);
        }
    }
}
