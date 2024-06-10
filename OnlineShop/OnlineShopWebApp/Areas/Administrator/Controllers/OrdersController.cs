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

        public async Task<IActionResult> Index()
        {
            var orders = await ordersRepository.GetAllAsync();
            return View(mapper.Map<List<OrderViewModel>>(orders));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var order = await ordersRepository.TryGetByIdAsync(id);
            return View(mapper.Map<OrderViewModel>(order));
        }

        public async Task<IActionResult> UpdateStatus(Guid id, OrderStatus status)
        {
            await ordersRepository.UpdateStatusAsync(id, status);
            return RedirectToAction(nameof(Index));
        }
    }
}