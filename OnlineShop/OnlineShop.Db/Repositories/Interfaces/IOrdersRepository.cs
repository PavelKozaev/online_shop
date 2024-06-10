using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        Task AddAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<Order> TryGetByIdAsync(Guid id);
        Task<List<Order>> GetOrdersByUserNameAsync(string userName);
        Task UpdateStatusAsync(Guid orderId, OrderStatus newStatus);
    }
}