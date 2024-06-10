using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Buy(UserDeliveryInfoViewModel user)
        {
            var existingCart = await cartsRepository.TryGetByUserNameAsync(User.Identity.Name);

            var order = new Order
            {
                Items = existingCart.Items,
                User = mapper.Map<UserDeliveryInfo>(user),
            };

            await ordersRepository.AddAsync(order);
            await cartsRepository.ClearAsync(User.Identity.Name);
            return RedirectToAction(nameof(ThankYouPage), new { id = order.Id });
        }

        public async Task<IActionResult> ThankYouPage(Guid id)
        {
            var order = await ordersRepository.TryGetByIdAsync(id);
            return View(mapper.Map<OrderViewModel>(order));
        }
    }
}