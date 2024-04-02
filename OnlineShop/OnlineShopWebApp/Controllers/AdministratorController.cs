using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IProductsRepository productsRepository;
        public AdministratorController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetOrders()
        {
            return View();
        }

        public IActionResult GetUsers()
        {
            return View();
        }

        public IActionResult GetRoles()
        {
            return View();
        }

        public IActionResult GetProducts()
        {
            var products = productsRepository.GetAll();

            if (products == null)
            {
                return NotFound("Книги не найдены");
            }

            return View(products);
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
                return RedirectToAction("GetProducts"); 
            }
            return View(product);
        }

        public IActionResult Update(Guid id)
        {
            var product = productsRepository.TryGetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        
        [HttpPost]
        public IActionResult Update(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productsRepository.Edit(product);
                    return RedirectToAction("GetProducts");
                }
                catch
                {                    
                }
            }
            return View(product);
        }

        public IActionResult Delete(Guid id)
        {
            var product = productsRepository.TryGetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            productsRepository.Remove(id);

            return RedirectToAction("GetProducts");
        }
    }
}
