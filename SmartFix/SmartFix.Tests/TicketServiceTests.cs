using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFix.Application.Services;
using SmartFix.Infrastructur.Data;
using Xunit;

namespace SmartFix.Tests
{
    public class TicketServiceTests
    {
        private SmartFixDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<SmartFixDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new SmartFixDbContext(options);
        }

        [Fact]
        public async Task CreateTicketAsync_ShouldCreateTicketAndLogEntry()
        {
            // Arrange (تجهيز التست)
            using var context = GetInMemoryDbContext();
            var service = new TicketService(context);

            // Act (تنفيذ العملية)
            var ticket = await service.CreateTicketAsync(1, 1, "الشاشة مكسورة وتحتاج استبدال", "High");

            // Assert (التحقق من النتيجة)
            Assert.NotNull(ticket);
            Assert.StartsWith("TCK-", ticket.TicketNumber);
            Assert.Equal("Pending", ticket.Status);

            // التحقق من إنشاء السجل التاريخي (Log)
            var logCount = await context.TicketLogs.CountAsync();
            Assert.Equal(1, logCount);
        }
    }
}
