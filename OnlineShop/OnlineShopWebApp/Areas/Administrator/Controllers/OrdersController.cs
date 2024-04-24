using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }
        public IActionResult Index()
        {
            var orders = ordersRepository.GetAll();
            return View(Mapping.ToOrderViewModels(orders));
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

        public IActionResult UpdateStatus(Guid id, OrderStatusViewModel status)
        {
            ordersRepository.UpdateStatus(id, Mapping.ToOrderStatus(status));
            return RedirectToAction(nameof(Index));
        }
    }
}