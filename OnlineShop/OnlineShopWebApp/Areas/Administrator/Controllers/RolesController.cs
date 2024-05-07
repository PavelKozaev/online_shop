using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Administrator.Models;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RolesController : Controller
    {
        private readonly IRolesRepository rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {            
            this.rolesRepository = rolesRepository;
        }

        public IActionResult Index()
        {
            var roles = rolesRepository.GetAll();

            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (rolesRepository.TryGetByName(role.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует!");
            }

            if (ModelState.IsValid)
            {
                rolesRepository.Add(role);
                return RedirectToAction(nameof(Index));
            }

            return View(role);
        }

        public IActionResult Delete(string roleName)
        {
            if (roleName == null)
            {
                return NotFound();
            }

            rolesRepository.Remove(roleName);

            return RedirectToAction(nameof(Index));
        }
    }
}
