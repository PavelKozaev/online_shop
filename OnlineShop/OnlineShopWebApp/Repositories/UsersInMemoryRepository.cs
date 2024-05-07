using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Repositories
{
    public class UsersInMemoryRepository : IUsersRepository
    {
        private readonly List<UserViewModel> users = [

            new UserViewModel("aaa@aaa.ru", "12345678", "Сальвадор", "Дали", "11111111111"),

            new UserViewModel("bbb@bbb.ru", "12345678", "Мари", "Экзюпери", "11111111111")
            ];

        public IEnumerable<UserViewModel> GetAll()
        {
            return users;
        }

        public UserViewModel TryGetById(Guid id)
        {
            return users.FirstOrDefault(user => user.Id == id);
        }

        public UserViewModel TryGetByEmail(string email)
        {
            return users.FirstOrDefault(user => user.Email == email);
        }

        public void Add(UserViewModel user)
        {
            users.Add(user);
        }

        public void Edit(UserViewModel user)
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