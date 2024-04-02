using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAll();
        Product TryGetById(Guid id);
        void Add(Product Product);
        void Edit(Product product);
        void Remove(Guid id);
    }
}