using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository productsRepository;

        public ProductController(IProductsRepository productRepository)
        {
            this.productsRepository = productRepository;
        }


        public IActionResult Index(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Неверный идентификатор книги");
            }

            var product = productsRepository.TryGetById(id);

            if (product == null)
            {
                return NotFound("Книга не найдена");
            }

            return View(product);
        }
    }
}
