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
		private readonly IMapper mapper;
        private readonly ILogger logger;

        public UsersController(UserManager<User> userManager, IMapper mapper, ILogger<UsersController> logger)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.logger = logger;
        }


        public IActionResult Index()
		{
            var users = userManager.Users.ToList();
            return View(mapper.Map<List<UserViewModel>>(users));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Register register)
        {
            if (register.UserName == register.Password)
            {
                ModelState.AddModelError("", "Имя пользователя и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                User user = new User { Email = register.Email, 
                                       UserName = register.UserName, 
                                       PhoneNumber = register.PhoneNumber };

                var result = userManager.CreateAsync(user, register.Password).Result;

                if (result.Succeeded)
                {
                    TryAssignUserRole(user);
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


        public void TryAssignUserRole(User user)
        {
            try
            {
                userManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке добавления роли для пользователя.");
            }
        }


        public IActionResult Edit(string name)
        {
            var user = userManager.FindByNameAsync(name).Result;
            return View(mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel, string name)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByNameAsync(name).Result;
                user.Email = userViewModel.Email;   
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.UserName = userViewModel.UserName;
                userManager.UpdateAsync(user).Wait();
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }


        public IActionResult Details(string name)
		{
            var user = userManager.FindByNameAsync(name).Result;
            return View(mapper.Map<UserViewModel>(user));
        }


		public IActionResult Delete(string name)
		{
            var user = userManager.FindByNameAsync(name).Result;
            userManager.DeleteAsync(user).Wait();
            return RedirectToAction(nameof(Index));
        }


		public IActionResult ChangePassword(string name)
		{
            var changePassword = new ChangePassword()
            {
                UserName = name
            };
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if (changePassword.UserName == changePassword.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать.");
            }

            if (ModelState.IsValid)
            {
                var user = userManager.FindByNameAsync(changePassword.UserName).Result;
                var newHashPassword = userManager.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newHashPassword;
                userManager.UpdateAsync(user).Wait();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(ChangePassword));
        }
    }
}
