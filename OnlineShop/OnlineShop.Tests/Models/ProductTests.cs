using OnlineShop.Db.Models;

namespace OnlineShop.Tests.Models
{
    public class ProductTests
    {
        [Fact]
        public void Product_Constructor_ShouldInitializeCollections()
        {
            // Arrange & Act
            var product = new Product();

            // Assert
            Assert.NotNull(product.Images);
            Assert.NotNull(product.CartItems);
            Assert.NotNull(product.Favorites);
        }

        [Fact]
        public void Product_Constructor_WithParameters_ShouldSetProperties()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            string productName = "Test Product";
            string productAuthor = "Test Author";
            decimal productCost = 100.0m;
            string productDescription = "Test description.";

            // Act
            var product = new Product(productId, productName, productAuthor, productCost, productDescription);

            // Assert
            Assert.Equal(productId, product.Id);
            Assert.Equal(productName, product.Name);
            Assert.Equal(productAuthor, product.Author);
            Assert.Equal(productCost, product.Cost);
            Assert.Equal(productDescription, product.Description);
        }
    }
}
