using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;

namespace OnlineShop.Db.Repositories
{
    public class OrdersDbRepository : IOrdersRepository
    {
        private readonly DatabaseContext databaseContext;


        public OrdersDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        public List<Order> GetAll() 
        {
            return databaseContext.Orders
                .AsNoTracking()
                .Include(x => x.UserDeliveryInfo)
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                .ToList();
        }


        public Order TryGetById(Guid id)
        {
            return databaseContext.Orders
                .AsNoTracking()
                .Include(cart => cart.UserDeliveryInfo)
                .Include(cart => cart.Items)
                    .ThenInclude(item => item.Product)
                .FirstOrDefault(o => o.Id == id);
        }


        public void Add(Order order)
        {      
            order.CreateDateTime = order.CreateDateTime.ToUniversalTime();
            
            databaseContext.Orders.Add(order);
            databaseContext.SaveChanges(); 
        }             


        public void UpdateStatus(Guid orderId, OrderStatus newStatus)
        {
            var order = TryGetById(orderId);

            if (order != null)
            {
                order.Status = newStatus;
            }

            databaseContext.SaveChanges();
        }
    }
}