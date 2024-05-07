using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
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
            var order = ordersRepository.TryGetById(id);
            var orderViewModel = mapper.Map<OrderViewModel>(order);
            return View(orderViewModel);
        }

        public IActionResult UpdateStatus(Guid id, OrderStatus status)
        {          
            ordersRepository.UpdateStatus(id, status);
            return RedirectToAction(nameof(Index));
        }
    }
}