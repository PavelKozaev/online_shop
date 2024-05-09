using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;

        public HomeController(IProductsRepository productsRepository, IMapper mapper)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }


        public IActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(mapper.Map<List<ProductViewModel>>(products));
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
