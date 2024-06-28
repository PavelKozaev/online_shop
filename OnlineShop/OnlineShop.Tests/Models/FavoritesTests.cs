using OnlineShop.Db.Models;

namespace OnlineShop.Tests.Models
{
    public class FavoritesTests
    {
        [Fact]
        public void Favorites_Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var user = new User
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Avatar = "avatar-url"
            };

            var favorites = new Favorites
            {
                User = user,
                Products = new List<Product>()
            };

            // Assert
            Assert.NotNull(favorites.Products); 
            Assert.Empty(favorites.Products); 
            Assert.Equal(user, favorites.User); 
        }
    }
}
