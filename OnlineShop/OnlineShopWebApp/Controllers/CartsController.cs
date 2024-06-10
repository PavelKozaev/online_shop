using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly ICartsRepository cartsRepository;
        private readonly IMapper mapper;

        public CartsController(IProductsRepository productsRepository, ICartsRepository cartsRepository, IMapper mapper)
        {
            this.productsRepository = productsRepository;
            this.cartsRepository = cartsRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var cart = await cartsRepository.TryGetByUserNameAsync(User.Identity.Name);
            return View(mapper.Map<CartViewModel>(cart));
        }

        public async Task<IActionResult> Create(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            await cartsRepository.AddAsync(product, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DecreaseAmount(Guid id)
        {
            await cartsRepository.DecreaseAmountAsync(id, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Clear()
        {
            await cartsRepository.ClearAsync(User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}