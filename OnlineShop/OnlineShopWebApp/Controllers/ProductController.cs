using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index(int id)
        {   
            var product = productRepository.GetById(id);

            if (product == null)
            {
                return NotFound("Продукт не найден");
            }

            return View(product);
        }
    }
}
