using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
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
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);

            var cartViewModel = mapper.Map<CartViewModel>(cart); 
            return View(cartViewModel);
        }


        public IActionResult Create(Guid id)
        {         
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var product = productsRepository.TryGetById(id);

            if (product == null)
            {
                return NotFound();
            }

            cartsRepository.Add(product, Constants.UserId);

            return RedirectToAction("Index");
        }


        public IActionResult DecreaseAmount(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            cartsRepository.DecreaseAmount(id, Constants.UserId);

            return RedirectToAction("Index");
        }


        public IActionResult Clear() 
        {
            cartsRepository.Clear(Constants.UserId);

            return RedirectToAction("Index");
        }
    }
}