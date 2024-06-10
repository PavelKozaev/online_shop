using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Administrator.Models;
using OnlineShopWebApp.Models;
using ChangePasswordViewModel = OnlineShopWebApp.Areas.Administrator.Models.ChangePasswordViewModel;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UsersController : Controller
	{
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public UsersController(UserManager<User> userManager, IMapper mapper, ILogger<UsersController> logger, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.logger = logger;
            this.roleManager = roleManager;
        }


        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            return View(mapper.Map<List<UserViewModel>>(users));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Register register)
        {
            if (register.UserName == register.Password)
            {
                ModelState.AddModelError("", "Имя пользователя и пароль не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = register.Email,
                    UserName = register.UserName,
                    PhoneNumber = register.PhoneNumber
                };

                var result = await userManager.CreateAsync(user, register.Password);

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

        public async Task TryAssignUserRole(User user)
        {
            try
            {
                await userManager.AddToRoleAsync(user, Constants.UserRoleName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при попытке добавления роли для пользователя.");
            }
        }

        public async Task<IActionResult> Edit(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            return View(mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userViewModel, string name)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(name);
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.UserName = userViewModel.UserName;
                await userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        public async Task<IActionResult> Details(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            return View(mapper.Map<UserViewModel>(user));
        }

        public async Task<IActionResult> Delete(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            await userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult EditRights(string name)
        {
            var user = userManager.FindByNameAsync(name).Result;
            var userRoles = userManager.GetRolesAsync(user).Result;
            var roles = roleManager.Roles.ToList();
            var model = new EditRightsViewModel
            {
                UserName = user.UserName,
                UserRoles = userRoles.Select(x => new RoleViewModel { Name = x }).ToList(),
                AllRoles = roles.Select(x => new RoleViewModel { Name = x.Name }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRights(string name, Dictionary<string, string> userRolesViewModel)
        {
            var userSelectedRoles = userRolesViewModel.Select(x => x.Key);
            var user = userManager.FindByNameAsync(name).Result;
            var userRoles = userManager.GetRolesAsync(user).Result;
            userManager.RemoveFromRolesAsync(user, userRoles).Wait();
            userManager.AddToRolesAsync(user, userSelectedRoles).Wait();
            return Redirect($"Details?name={name}");
        }


        public IActionResult ChangePassword(string name)
		{
            var changePassword = new ChangePasswordViewModel()
            {
                UserName = name
            };
            return View(changePassword);
        }

        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (changePassword.UserName.Equals(changePassword.Password, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать.");
            }

            if (!ModelState.IsValid)
            {
                return View(changePassword);
            }

            var user = userManager.FindByNameAsync(changePassword.UserName).Result;

            if (user != null)
            {
                var verificationResult = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, changePassword.Password);
                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    var newHashPassword = userManager.PasswordHasher.HashPassword(user, changePassword.Password);
                    user.PasswordHash = newHashPassword;
                    var result = userManager.UpdateAsync(user).Result;

                    if (result.Succeeded)
                    {

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
                else
                {
                    ModelState.AddModelError("", "Новый пароль не должен быть таким же, как старый.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден.");
            }

            return View(changePassword);
        }
    }
}
