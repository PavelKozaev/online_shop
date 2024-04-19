using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;

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

            return View(products);
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
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                productsRepository.Add(product);
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

            return View(product);
        }


        [HttpPost]
        public IActionResult Edit(Product product)
        {     
            if (ModelState.IsValid)
            {

                productsRepository.Edit(product);
                return RedirectToAction(nameof(Index));

            }

            return View(product);
        }
        

        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            productsRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
