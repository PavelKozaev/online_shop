using OnlineShopWebApp.Areas.Administrator.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAll();
        User TryGetById(Guid id);
        User TryGetByEmail(string email);
        void Add(User user);
        void Edit(User user);
        void Remove(Guid id);
        void ChangePassword(Guid id, string password);
        void ChangeRole(Guid id, string role);
    }
}
