using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class CartsController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly ICartsRepository cartsRepository;


        public CartsController(IProductsRepository productsRepository, ICartsRepository cartsRepository)
        {
            this.productsRepository = productsRepository;
            this.cartsRepository = cartsRepository;
        }


        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);

            return View(Mapping.ToCartViewModel(cart));
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