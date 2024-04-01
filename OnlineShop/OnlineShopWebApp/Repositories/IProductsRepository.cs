using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAll();
        Product TryGetById(Guid id);
        void Add(Product product);
        void Edit(Product product);
        void Delete(Product product);
    }
}