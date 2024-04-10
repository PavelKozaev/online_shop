using OnlineShopWebApp.Repositories.Interfaces;

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
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            var user = TryGetById(id);

            users.Remove(user);
        }
    }
}
