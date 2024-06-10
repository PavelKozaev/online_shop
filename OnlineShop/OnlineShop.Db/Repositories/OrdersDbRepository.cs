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

        public async Task<List<Order>> GetAllAsync()
        {
            return await databaseContext.Orders
                .Include(x => x.User)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .ToListAsync();
        }

        public async Task<Order> TryGetByIdAsync(Guid id)
        {
            return await databaseContext.Orders
                .Include(x => x.User)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            await databaseContext.Orders.AddAsync(order);
            await databaseContext.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(Guid orderId, OrderStatus newStatus)
        {
            var order = await TryGetByIdAsync(orderId);

            if (order != null)
            {
                order.Status = newStatus;
                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetOrdersByUserNameAsync(string userName)
        {
            return await databaseContext.Orders
                .Include(x => x.User)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Where(x => x.User.Name == userName)
                .ToListAsync();
        }
    }
}