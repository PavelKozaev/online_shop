using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;

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
            var product = productsRepository.TryGetById(id);
            return View(product.ToProductViewModel());
        }
    }
}