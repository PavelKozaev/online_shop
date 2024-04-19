using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
	[Area("Administrator")]
	public class UsersController : Controller
	{
		private readonly IUsersRepository usersRepository;
		private readonly IRolesRepository rolesRepository;

		public UsersController(IUsersRepository usersRepository, IRolesRepository rolesRepository)
		{
			this.usersRepository = usersRepository;
			this.rolesRepository = rolesRepository;
		}


		public IActionResult Index()
		{
			var users = usersRepository.GetAll();

            if (users == null)
            {
                return NotFound();
            }

            return View(users);
		}


		public IActionResult Details(Guid id)
		{
			if (id == Guid.Empty)
			{
				return NotFound();
			}

			var user = usersRepository.TryGetById(id);

			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		public IActionResult Create(Register register)
		{
			var userAccount = usersRepository.TryGetByEmail(register.Email);

			if (userAccount != null)
			{
				ModelState.AddModelError("", "Пользователь с таким именем уже есть.");
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

			return RedirectToAction(nameof(Index));
		}


		public IActionResult Edit(Guid id)
		{
			if (id == Guid.Empty)
			{
				return NotFound();
			}

			var user = usersRepository.TryGetById(id);

			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}


		[HttpPost]
		public IActionResult Edit(User user)
		{
			if (ModelState.IsValid)
			{
				usersRepository.Edit(user);

				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}


		public IActionResult Delete(Guid id)
		{
			usersRepository.Remove(id);

			return RedirectToAction(nameof(Index));
		}


		public IActionResult ChangePassword(Guid id)
		{
			if (id == Guid.Empty)
			{
				return NotFound();
			}

			var user = usersRepository.TryGetById(id);

			if (user == null)
			{
                return NotFound();
            }

			ViewData["id"] = id;
			ViewData["email"] = user.Email;

			return View();
		}

		[HttpPost]
		public IActionResult ChangePassword(Guid id, string password, string confirmPassword)
		{
			if (password != confirmPassword)
			{
				ModelState.AddModelError("", "Пароли не совпадают.");

				return View();
			}

			if (ModelState.IsValid)
			{
				usersRepository.ChangePassword(id, password);

				return RedirectToAction(nameof(Index));
			}

			return View();
		}


		public IActionResult ChangeRole(Guid id)
		{
			if (id == Guid.Empty)
			{
				return NotFound();
			}

			var user = usersRepository.TryGetById(id);

			var roles = rolesRepository.GetAll();

			if (user == null || roles == null)
			{
				return NotFound();
			}

			ViewData["id"] = id;
			ViewData["email"] = user.Email;
			ViewData["role"] = user.Role.Name;

			return View(roles);
		}


		[HttpPost]
		public IActionResult ChangeRole(Guid id, string role)
		{
			if (id == Guid.Empty || role == null)
			{
				return NotFound();
			}

			usersRepository.ChangeRole(id, role);

			return RedirectToAction(nameof(Index));
		}
	}
}
