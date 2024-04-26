using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IMapper mapper;


        public OrdersController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository, IMapper mapper)
        {
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
            this.mapper = mapper;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Buy(UserDeliveryInfoViewModel user)
        {
            var existingCart = cartsRepository.TryGetByUserId(Constants.UserId);

            var order = new Order
            {
                Items = existingCart.Items,
                User = mapper.Map<UserDeliveryInfo>(user),
            };
            ordersRepository.Add(order);

            cartsRepository.Clear(Constants.UserId);

            
            return View();
        }
    }
}