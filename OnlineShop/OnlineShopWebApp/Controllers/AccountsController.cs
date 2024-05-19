﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IOrdersRepository ordersRepository;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountsController> logger, IMapper mapper, IOrdersRepository ordersRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.mapper = mapper;
            this.ordersRepository = ordersRepository;
        }        


        public IActionResult Login(string returnUrl)
        {
            return View(new Login() { ReturnUrl = returnUrl });
        }


        [HttpPost]
        public IActionResult Login(Login login) 
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(login.Email, 
                                                               login.Password, 
                                                               login.IsRememberMe, 
                                                               false).Result;
                if (result.Succeeded)
                {
                    return Redirect(login.ReturnUrl ?? "/Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
            }

            return View(login);
        }


        public IActionResult Register(string returnUrl)
        {
            return View(new Register() { ReturnUrl = returnUrl });
        }


        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (register.Email == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
                return View(register);
            }

            if (ModelState.IsValid)
            {
                User user = new User { Email = register.Email,
                                       UserName = register.UserName,
                                       PhoneNumber = register.PhoneNumber};

                var result = userManager.CreateAsync(user, register.Password).Result;

                if (result.Succeeded)
                {
                    signInManager.SignInAsync(user, false).Wait();

                    TryAssignUserRole(user);

                    return Redirect(register.ReturnUrl ?? "/Home");
                }
                else
                {
                    foreach(var error  in result.Errors)
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

        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        public async Task<IActionResult> Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);
                if (user != null)
                {
                    var model = new UserViewModel
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber
                    };

                    return View(model);
                }
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }


        public IActionResult EditProfile()
        {
            var userName = User.Identity.Name;
            var user = userManager.FindByNameAsync(userName).Result;
            return View(mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public IActionResult EditProfile(UserViewModel userViewModel)
        {
            var userName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var user = userManager.FindByNameAsync(userName).Result;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.UserName = userViewModel.UserName;
                userManager.UpdateAsync(user).Wait();
                return RedirectToAction(nameof(Profile));
            }
            return View(userViewModel);
        }


        public IActionResult UserOrders()
        {
            var userName = User.Identity.Name; 
            var orders = ordersRepository.GetOrdersByUserName(userName);                        
            return View(mapper.Map<List<OrderViewModel>>(orders));
        }


        public IActionResult ChangeUserPassword()
        {
            var changePassword = new ChangeUserPasswordViewModel()
            {
                UserName = User.Identity.Name
            };
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangeUserPassword(ChangeUserPasswordViewModel changePassword)
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
                return RedirectToAction(nameof(Profile));
            }

            return RedirectToAction(nameof(ChangeUserPassword));
        }
    }
}