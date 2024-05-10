using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartRepository;
        private readonly IMapper mapper;
        public CartViewComponent(ICartsRepository cartRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }
        public IViewComponentResult Invoke()
        {
            var cart = cartRepository.TryGetByUserId(User.Identity.Name);

            var cartViewModel = mapper.Map<CartViewModel>(cart);

            var productCounts = cartViewModel?.Amount ?? 0;

            return View("Cart", productCounts);
        }        
    }
}
