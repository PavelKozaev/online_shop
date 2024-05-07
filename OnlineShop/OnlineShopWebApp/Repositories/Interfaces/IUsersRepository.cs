using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<UserViewModel> GetAll();
        UserViewModel TryGetById(Guid id);
        UserViewModel TryGetByEmail(string email);
        void Add(UserViewModel user);
        void Edit(UserViewModel user);
        void Remove(Guid id);
        void ChangePassword(Guid id, string password);
        void ChangeRole(Guid id, string role);
    }
}
