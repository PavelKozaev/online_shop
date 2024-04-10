using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class UserController : Controller
    {
        private readonly IUsersRepository usersRepository;
        public UserController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        public IActionResult Index()
        {
            var products = usersRepository.GetAll();

            return View(products);
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Неверный идентификатор пользователя");
            }

            var user = usersRepository.TryGetById(id);

            if (user == null)
            {
                return NotFound("Пользователь не найдена");
            }

            return View(user);
        }
    }
}
