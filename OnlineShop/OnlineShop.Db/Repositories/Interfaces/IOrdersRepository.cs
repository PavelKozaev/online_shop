using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        void Add(Order order);
        List<Order> GetAll();
        Order TryGetById(Guid id);
        List<Order> GetOrdersByUserName(string userName);
        void UpdateStatus(Guid orderId, OrderStatus newStatus);
    }
}