using OnlineShop.Db.Models;

namespace OnlineShop.Tests.Models
{
    public class OrderTests
    {
        [Fact]
        public void Order_Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var order = new Order();

            // Assert
            Assert.Equal(OrderStatus.Created, order.Status); 
            Assert.True(order.CreateDateTime < DateTime.UtcNow); 
        }
    }
}
