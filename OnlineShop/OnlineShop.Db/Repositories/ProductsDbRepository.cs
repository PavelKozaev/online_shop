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

        public List<Product> GetAll() => databaseContext.Products.ToList();

        public Product TryGetById(Guid id) => databaseContext.Products.FirstOrDefault(product => product.Id == id);


        public void Add(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public void Edit(Product product)
        {
            var existingProduct = databaseContext.Products.FirstOrDefault(x => x.Id == product.Id);

            if (existingProduct == null)
            {
                return;
            }

            existingProduct.Name = product.Name;
            existingProduct.Author = product.Author;
            existingProduct.Cost = product.Cost;
            existingProduct.Description = product.Description;
            existingProduct.ImagePath = product.ImagePath;

            databaseContext.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var product = TryGetById(id);

            databaseContext.Products.Remove(product);

            databaseContext.SaveChanges();
        }
    }
}
