using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Administrator.Models;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RolesController : Controller
    {
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;

        public RolesController(RoleManager<Role> roleManager, IMapper mapper)
        {            
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(mapper.Map<List<RoleViewModel>>(roles));
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(RoleViewModel role)
        {
            var result = roleManager.CreateAsync(new Role(role.Name)).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(role);
        }

        public IActionResult Delete(string roleName)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;
            if (role != null)
            {
                roleManager.DeleteAsync(role).Wait();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
