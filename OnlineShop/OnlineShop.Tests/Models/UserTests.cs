using OnlineShop.Db.Models;

namespace OnlineShop.Tests.Models
{
    public class UserTests
    {
        [Fact]
        public void User_Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var user = new User
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Avatar = "avatar-url"
            };

            // Assert
            Assert.Equal("testuser", user.UserName);
            Assert.Equal("testuser@example.com", user.Email);
            Assert.Equal("avatar-url", user.Avatar);            
        }
    }
}
