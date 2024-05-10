using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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

        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}