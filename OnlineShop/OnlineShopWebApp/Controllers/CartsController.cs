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


        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(User.Identity.Name);
            var cartViewModel = mapper.Map<CartViewModel>(cart); 
            return View(cartViewModel);
        }


        public IActionResult Create(Guid id)
        {        
            var product = productsRepository.TryGetById(id);
            cartsRepository.Add(product, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult DecreaseAmount(Guid id)
        {
            cartsRepository.DecreaseAmount(id, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Clear() 
        {
            cartsRepository.Clear(User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}