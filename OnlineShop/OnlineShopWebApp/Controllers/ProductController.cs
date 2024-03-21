using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid product identifier");
            }

            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            return View(product);
        }

    }
}
