using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUsersRepository usersRepository;

        public AccountsController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(Login login) 
        {
            var userAccount = usersRepository.TryGetByEmail(login.Email);

            if (userAccount == null)
            {
                ModelState.AddModelError("", "Пользователь с таким именем не найден. Проверьте имя или зарегистрируйтесь.");
                return View(login);
            }

            if (userAccount.Password != login.Password)
            {
                ModelState.AddModelError("", "Неверный пароль");
                return View(login);
            }

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(Register register)
        {
            var userAccount = usersRepository.TryGetByEmail(register.Email);
            if (userAccount != null)
            {
                ModelState.AddModelError("", "Пользователь с таким Email уже есть.");
                return View(register);
            }

            if (register.Email == register.Password)
            {
                ModelState.AddModelError("", "Имя пользователя и пароль не должны совпадать");
                return View(register);
            }

            if (!ModelState.IsValid)
            {
                return View(register);
            }

            usersRepository.Add(new User(register.Email, register.Password, register.FirstName, register.LastName, register.Phone));

            return RedirectToAction("Index", "Home");
        }
    }
}
