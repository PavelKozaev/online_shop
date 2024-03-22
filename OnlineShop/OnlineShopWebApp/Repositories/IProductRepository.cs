using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllById();
        Product GetById(int id);
    }
}