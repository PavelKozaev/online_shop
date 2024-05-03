using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            var product = productsRepository.TryGetById(id);
            var productViewModel = mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }
    }
}