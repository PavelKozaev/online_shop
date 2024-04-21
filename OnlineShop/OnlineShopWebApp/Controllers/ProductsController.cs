using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productsRepository;


        public ProductsController(IProductsRepository productRepository)
        {
            this.productsRepository = productRepository;
        }


        public IActionResult Index(Guid id)
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

            return View(product);
        }
    }
}
