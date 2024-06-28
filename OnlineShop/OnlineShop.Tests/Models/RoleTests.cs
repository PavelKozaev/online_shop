using OnlineShop.Db.Models;

namespace OnlineShop.Tests.Models
{
    public class RoleTests
    {
        [Fact]
        public void Role_Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var roleName = "admin";
            var role = new Role(roleName);

            // Assert
            Assert.Equal(roleName, role.Name); 
        }
    }
}
