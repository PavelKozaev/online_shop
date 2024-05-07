using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UsersController : Controller
	{
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
		private readonly IMapper mapper;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public IActionResult Index()
		{
            var users = userManager.Users.ToList();
            var userViewModels = mapper.Map<List<UserViewModel>>(users);
            return View(userViewModels);
        }


		public IActionResult Details(string name)
		{
            var user = userManager.FindByNameAsync(name).Result;
            var userViewModel = mapper.Map<List<UserViewModel>>(user);
            return View(userViewModel);
        }

		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		public IActionResult Create(Register register)
		{
            if (register.Email == register.Password)
            {
                ModelState.AddModelError("", "Имя пользователя и пароль не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                User user = new User { Email = register.Email, UserName = register.FirstName, PhoneNumber = register.Phone };
                var result = userManager.CreateAsync(user, register.Password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(register);
		}


        public IActionResult Edit(string name)
        {
            var user = userManager.FindByNameAsync(name).Result;
            var userViewModel = mapper.Map<List<UserViewModel>>(user);
            return View(userViewModel);
        }


  //      [HttpPost]
		//public IActionResult Edit(UserViewModel user)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		usersRepository.Edit(user);

		//		return RedirectToAction(nameof(Index));
		//	}

		//	return RedirectToAction(nameof(Index));
		//}


		public IActionResult Delete(string name)
		{
            var user = userManager.FindByNameAsync(name).Result;
            userManager.DeleteAsync(user).Wait();
            return RedirectToAction(nameof(Index));
        }


		//public IActionResult ChangePassword(Guid id)
		//{
		//	if (id == Guid.Empty)
		//	{
		//		return NotFound();
		//	}

		//	var user = usersRepository.TryGetById(id);

		//	if (user == null)
		//	{
  //              return NotFound();
  //          }

		//	ViewData["id"] = id;
		//	ViewData["email"] = user.Email;

		//	return View();
		//}

		//[HttpPost]
		//public IActionResult ChangePassword(Guid id, string password, string confirmPassword)
		//{
		//	if (password != confirmPassword)
		//	{
		//		ModelState.AddModelError("", "Пароли не совпадают.");

		//		return View();
		//	}

		//	if (ModelState.IsValid)
		//	{
		//		usersRepository.ChangePassword(id, password);

		//		return RedirectToAction(nameof(Index));
		//	}

		//	return View();
		//}


		//public IActionResult ChangeRole(Guid id)
		//{
		//	if (id == Guid.Empty)
		//	{
		//		return NotFound();
		//	}

		//	var user = usersRepository.TryGetById(id);

		//	var roles = rolesRepository.GetAll();

		//	if (user == null || roles == null)
		//	{
		//		return NotFound();
		//	}

		//	ViewData["id"] = id;
		//	ViewData["email"] = user.Email;
		//	ViewData["role"] = user.Role.Name;

		//	return View(roles);
		//}


		//[HttpPost]
		//public IActionResult ChangeRole(Guid id, string role)
		//{
		//	if (id == Guid.Empty || role == null)
		//	{
		//		return NotFound();
		//	}

		//	usersRepository.ChangeRole(id, role);

		//	return RedirectToAction(nameof(Index));
		//}
	}
}
