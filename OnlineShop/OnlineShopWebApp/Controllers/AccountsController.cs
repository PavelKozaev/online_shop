using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;
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
        private readonly ImagesProvider imagesProvider;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountsController> logger, IMapper mapper, IOrdersRepository ordersRepository, ImagesProvider imagesProvider)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.mapper = mapper;
            this.ordersRepository = ordersRepository;
            this.imagesProvider = imagesProvider;
        }        


        public IActionResult Login(string returnUrl)
        {
            return View(new Login() { ReturnUrl = returnUrl });
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login login) 
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(login.Email, 
                                                               login.Password, 
                                                               login.IsRememberMe, 
                                                               false);
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
        public async Task<IActionResult> Register(Register register)
        {
            if (register.Email == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
                return View(register);
            }

            if (ModelState.IsValid)
            {
                var user = new User 
                {   Email = register.Email,
                    UserName = register.UserName,
                    PhoneNumber = register.PhoneNumber
                };

                var result = await userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    await TryAssignUserRoleAsync(user);
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

        private async Task TryAssignUserRoleAsync(User user)
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

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
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
                        PhoneNumber = user.PhoneNumber,
                        Avatar = user.Avatar,
                    };

                    return View(model);
                }
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }


        public async Task<IActionResult> EditProfile()
        {
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            return View(mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel userViewModel)
        {
            var userName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(userName);
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.UserName = userViewModel.UserName;

                if (userViewModel.UploadedFile != null)
                {
                    // Если файл был загружен, сохраняем его и обновляем ссылку на аватар в профиле
                    var avatarPath = imagesProvider.SaveFile(userViewModel.UploadedFile, ImageFolders.Profiles);
                    user.Avatar = avatarPath;
                }

                await userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Profile));
            }
            return View(userViewModel);
        }


        public async Task<IActionResult> UserOrders()
        {
            var userName = User.Identity.Name;
            var orders = await ordersRepository.GetOrdersByUserNameAsync(userName);
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
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordViewModel changePassword)
        {
            if (changePassword.UserName.Equals(changePassword.Password, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать.");
            }

            if (!ModelState.IsValid)
            {                
                return View(changePassword);
            }

            var user = await userManager.FindByNameAsync(changePassword.UserName);

            if (user != null)
            {
                var verificationResult = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, changePassword.Password);
                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    var newHashPassword = userManager.PasswordHasher.HashPassword(user, changePassword.Password);
                    user.PasswordHash = newHashPassword;
                    var result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        
                        return RedirectToAction(nameof(Profile));
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