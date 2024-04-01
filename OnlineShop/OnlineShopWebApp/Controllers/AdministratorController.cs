using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IProductsRepository productsRepository;
        public AdministratorController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetOrders()
        {
            return View();
        }

        public IActionResult GetUsers()
        {
            return View();
        }

        public IActionResult GetRoles()
        {
            return View();
        }

        public IActionResult GetProducts()
        {
            var products = productsRepository.GetAll();

            if (products == null)
            {
                return NotFound("Книги не найдены");
            }

            return View(products);
        }
    }
}
