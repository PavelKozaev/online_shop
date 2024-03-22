using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product TryGetById(Guid id);
    }
}