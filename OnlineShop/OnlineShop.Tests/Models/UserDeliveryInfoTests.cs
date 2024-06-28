using OnlineShop.Db.Models;

namespace OnlineShop.Tests.Models
{
    public class UserDeliveryInfoTests
    {
        [Fact]
        public void UserDeliveryInfo_Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var userDeliveryInfo = new UserDeliveryInfo
            {
                Name = "John Doe",
                EMail = "john.doe@example.com",
                Phone = "123456789",
                Address = "123 Main St"
            };

            // Assert
            Assert.Equal("John Doe", userDeliveryInfo.Name);
            Assert.Equal("john.doe@example.com", userDeliveryInfo.EMail);
            Assert.Equal("123456789", userDeliveryInfo.Phone);
            Assert.Equal("123 Main St", userDeliveryInfo.Address);
        }
    }
}
