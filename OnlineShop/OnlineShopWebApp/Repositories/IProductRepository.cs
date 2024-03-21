using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public Product GetProductById(Guid id);
    }
}