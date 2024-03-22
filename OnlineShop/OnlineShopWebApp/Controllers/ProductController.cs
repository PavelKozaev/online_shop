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

        public IActionResult Index(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Неверный идентификатор продукта");
            }

            var product = productRepository.TryGetById(id);

            if (product == null)
            {
                return NotFound("Продукт не найден");
            }

            return View(product);
        }
    }
}
