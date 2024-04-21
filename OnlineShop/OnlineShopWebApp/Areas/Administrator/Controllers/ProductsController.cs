using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productsRepository;


        public ProductsController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }


        public IActionResult Index()
        {
            var products = productsRepository.GetAll();

            if (products == null)
            {
                return NotFound();
            }
                        
            return View(Mapping.ToProductViewModels(products));
        }


        public IActionResult Details(Guid id)
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


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var productDb = new Product
                {
                    Name = product.Name,
                    Author = product.Author,
                    Cost = product.Cost,
                    Description = product.Description,
                    ImagePath = product.ImagePath,
                };

                productsRepository.Add(productDb);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
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

            return View(Mapping.ToProductViewModel(product));
        }


        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {     
            if (ModelState.IsValid)
            {
                var productDb = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Author = product.Author,
                    Cost = product.Cost,
                    Description = product.Description,
                    ImagePath = product.ImagePath,
                };

                productsRepository.Edit(productDb);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
        

        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            productsRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
