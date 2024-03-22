using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
    }
}