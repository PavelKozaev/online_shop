using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;


        public ProductsController(IProductsRepository productRepository, IMapper mapper)
        {
            this.productsRepository = productRepository;
            this.mapper = mapper;
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

            var productViewModel = mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }
    }
}