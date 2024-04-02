using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly ICartsRepository cartsRepository;

        public CartController(IProductsRepository productsRepository, ICartsRepository cartsRepository)
        {
            this.productsRepository = productsRepository;
            this.cartsRepository = cartsRepository;
        }

        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);
            return View(cart);
        }

        public IActionResult Add(Guid productId)
        {            
            var product = productsRepository.TryGetById(productId);

            cartsRepository.Add(product, Constants.UserId);

            return RedirectToAction("Index");
        }

        public IActionResult DecreaseAmount(Guid productId)
        {            
            cartsRepository.DecreaseAmount(productId, Constants.UserId);

            return RedirectToAction("Index");
        }

        public IActionResult Clear() 
        {
            cartsRepository.Clear(Constants.UserId);

            return RedirectToAction("Index");
        }
    }
}
