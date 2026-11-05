using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFix.Application.Services;
using SmartFix.Infrastructur.Data;
using Xunit;

namespace SmartFix.Tests
{
    public class AuthServiceTests
    {
        private SmartFixDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<SmartFixDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new SmartFixDbContext(options);
        }

        [Fact]
        public async Task RegisterAndLogin_ShouldSucceedWithCorrectPassword()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new AuthService(context);

            // Act
            var user = await service.RegisterAsync("tester", "test@test.com", "SecurePass123", "Customer", "0500000000");
            var loggedInUser = await service.LoginAsync("test@test.com", "SecurePass123");

            // Assert
            Assert.NotNull(user);
            Assert.NotNull(loggedInUser);
            Assert.Equal("tester", loggedInUser.Username);
        }

        [Fact]
        public async Task Login_ShouldFailWithWrongPassword()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new AuthService(context);
            await service.RegisterAsync("tester", "test@test.com", "SecurePass123", "Customer", "0500000000");

            // Act
            var loggedInUser = await service.LoginAsync("test@test.com", "WrongPassword");

            // Assert
            Assert.Null(loggedInUser);
        }
    }
}