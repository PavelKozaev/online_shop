using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;

namespace OnlineShop.Db.Repositories
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await databaseContext.Products.Include(x => x.Images).ToListAsync();
        }

        public async Task<Product> TryGetByIdAsync(Guid id)
        {
            return await databaseContext.Products.Include(x => x.Images).FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await databaseContext.Products.AddAsync(product);
            await databaseContext.SaveChangesAsync();
        }

        public async Task EditAsync(Product product)
        {
            var existingProduct = await databaseContext.Products.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == product.Id);

            if (existingProduct == null)
            {
                return;
            }

            existingProduct.Name = product.Name;
            existingProduct.Author = product.Author;
            existingProduct.Cost = product.Cost;
            existingProduct.Description = product.Description;

            foreach (var image in product.Images)
            {
                image.ProductId = product.Id;
                await databaseContext.Images.AddAsync(image);
            }

            await databaseContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var product = await TryGetByIdAsync(id);

            databaseContext.Products.Remove(product);
            await databaseContext.SaveChangesAsync();
        }
    }
}
