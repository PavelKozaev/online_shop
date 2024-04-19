using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;

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
            return View(orders);
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

        public IActionResult UpdateStatus(Guid id, OrderStatus status)
        {
            ordersRepository.UpdateStatus(id, status);
            return RedirectToAction(nameof(Index));
        }
    }
}
