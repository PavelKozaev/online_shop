using OnlineShop.Db.Models;

namespace OnlineShop.Tests.Models
{
    public class CartItemTests
    {
        [Fact]
        public void CartItem_Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var cartItem = new CartItem();

            // Assert
            Assert.Equal(default(Guid), cartItem.Id);
            Assert.Null(cartItem.Product);
            Assert.Null(cartItem.Cart);
            Assert.Equal(0, cartItem.Amount);
        }
    }
}
