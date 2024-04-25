using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IMapper mapper;

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            this.ordersRepository = ordersRepository;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var orders = ordersRepository.GetAll();
            var orderViewModels = mapper.Map<List<OrderViewModel>>(orders);
            return View(orderViewModels);
        }        

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var order = ordersRepository.TryGetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult UpdateStatus(Guid id, OrderStatusViewModel statusViewModel)
        {
            var status = mapper.Map<OrderStatus>(statusViewModel);

            ordersRepository.UpdateStatus(id, status);
            return RedirectToAction(nameof(Index));
        }
    }
}