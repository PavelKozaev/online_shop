using OnlineShopWebApp.Areas.Administrator.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        List<Role> GetAll();
        Role TryGetByName(string name);
        void Add(Role role);
        void Remove(string name);
    }
}
