using OnlineShopWebApp.Areas.Administrator.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;
using System.Data;

namespace OnlineShopWebApp.Repositories
{
    public class UsersInMemoryRepository : IUsersRepository
    {
        private readonly List<User> users = [

            new User("aaa@aaa.ru", "12345678", "Сальвадор", "Дали", "11111111111"),

            new User("bbb@bbb.ru", "12345678", "Мари", "Экзюпери", "11111111111")
            ];

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User TryGetById(Guid id)
        {
            return users.FirstOrDefault(user => user.Id == id);
        }

        public User TryGetByEmail(string email)
        {
            return users.FirstOrDefault(user => user.Email == email);
        }

        public void Add(User user)
        {
            users.Add(user);
        }

        public void Edit(User user)
        {
            var index = users.FindIndex(f => f.Id == user.Id);
            users[index] = user;
        }

        public void Remove(Guid id)
        {
            var user = TryGetById(id);

            users.Remove(user);
        }

		public void ChangePassword(Guid id, string password)
		{
			var user = TryGetById(id);
			if (user != null)
			{
				user.Password = password;
			}
		}

		public void ChangeRole(Guid id, string role)
        {
            var user = TryGetById(id);
            if (user != null)
            {
                user.Role.Name = role;
            }
        }
	}
}