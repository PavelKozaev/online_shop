using OnlineShop.Db.Models;

namespace OnlineShop.Tests.Models
{
    public class CartTests
    {
        [Fact]
        public void Cart_Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var user = new User
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Avatar = "avatar-url"
            };

            var cart = new Cart
            {
                User = user
            };

            // Assert
            Assert.NotNull(cart.Items); 
            Assert.Empty(cart.Items); 
            Assert.Equal(user, cart.User); 
        }
    }
}
