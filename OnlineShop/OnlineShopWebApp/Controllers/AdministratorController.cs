using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IRolesRepository rolesRepository;
        public AdministratorController(IProductsRepository productsRepository, IOrdersRepository ordersRepository, IRolesRepository rolesRepository)
        {
            this.productsRepository = productsRepository;
            this.ordersRepository = ordersRepository;
            this.rolesRepository = rolesRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetOrders()
        {
            var orders = ordersRepository.GetAll();
            return View(orders);
        }

        public IActionResult OrderDetails(Guid orderId)
        {
            var order = ordersRepository.TryGetById(orderId);
            return View(order);
        }

        public IActionResult UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            ordersRepository.UpdateStatus(orderId, status);
            return RedirectToAction("GetOrders");
        }

        public IActionResult GetUsers()
        {
            return View();
        }

        public IActionResult GetRoles()
        {
            var roles = rolesRepository.GetAll();
            return View(roles);
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(Role role)
        {
            if (rolesRepository.TryGetByName(role.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует!");
            }

            if (ModelState.IsValid)
            {
                rolesRepository.Add(role);
                return RedirectToAction("GetRoles");
            }            

            return View(role);
        }

        public IActionResult RemoveRole(string roleName)
        {
            rolesRepository.Remove(roleName);
            return RedirectToAction("GetRoles");
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
