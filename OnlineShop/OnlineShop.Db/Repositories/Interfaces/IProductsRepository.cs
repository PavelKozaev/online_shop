using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> TryGetByIdAsync(Guid id);
        Task AddAsync(Product Product);
        Task EditAsync(Product product);
        Task RemoveAsync(Guid id);
    }
}