using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;


        public HomeController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }


        public IActionResult Index()
        {
            var products = productsRepository.GetAll();

            if (products == null)
            {
                return NotFound();
            }
                        
            return View(Mapping.ToProductViewModels(products));
        }


        [HttpGet]
        public IActionResult Search(string name)
        {
            if (name != null)
            {
                var products = productsRepository.GetAll();
                var findProducts = products.Where(product => product.Name.ToLower().Contains(name.ToLower())).ToList();
                return View(findProducts);
            }
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
