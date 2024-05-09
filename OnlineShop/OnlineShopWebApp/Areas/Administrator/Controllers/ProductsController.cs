using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;

        public ProductsController(IProductsRepository productsRepository, IMapper mapper)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }


        public IActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(mapper.Map<List<ProductViewModel>>(products));
        }


        public IActionResult Details(Guid id)
        {
            var product = productsRepository.TryGetById(id);
            return View(mapper.Map<ProductViewModel>(product));
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                productsRepository.Add(mapper.Map<Product>(productViewModel));
                return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }


        public IActionResult Edit(Guid id)
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

            return View(mapper.Map<ProductViewModel>(product));
        }


        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var productDb = productsRepository.TryGetById(productViewModel.Id);
                if (productDb == null)
                {
                    return NotFound();
                }

                mapper.Map(productViewModel, productDb);
                productsRepository.Edit(productDb);
                return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }
        

        public IActionResult Delete(Guid id)
        {
            productsRepository.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
