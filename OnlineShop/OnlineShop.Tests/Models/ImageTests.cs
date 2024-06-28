using OnlineShop.Db.Models;

namespace OnlineShop.Tests.Models
{
    public class ImageTests
    {
        [Fact]
        public void Image_Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var image = new Image();

            // Assert
            Assert.Equal(default(Guid), image.Id);
            Assert.Null(image.Url);
            Assert.Equal(default(Guid), image.ProductId);
            Assert.Null(image.Product);
        }
    }
}
