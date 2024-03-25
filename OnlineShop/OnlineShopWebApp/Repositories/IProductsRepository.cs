using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAll();
        Product TryGetById(Guid id);
    }
}