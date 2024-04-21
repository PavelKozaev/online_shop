using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IOrdersRepository ordersRepository;


        public OrdersController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository)
        {
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Buy(UserDeliveryInfo user)
        {
            var existingCart = cartsRepository.TryGetByUserId(Constants.UserId);

            var existingCartViewModel = Mapping.ToCartViewModel(existingCart);

            var order = new Order
            {
                User = user,
                Items = existingCartViewModel.Items,
            };

            ordersRepository.Add(order);

            cartsRepository.Clear(Constants.UserId);

            return View(order);
        }
    }
}
