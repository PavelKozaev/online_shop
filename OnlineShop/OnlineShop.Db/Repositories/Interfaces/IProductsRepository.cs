using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product TryGetById(Guid id);
        void Add(Product Product);
        void Edit(Product product);
        void Remove(Guid id);
    }
}