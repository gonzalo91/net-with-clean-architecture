using Microsoft.EntityFrameworkCore;
using Shouldly;
using Zalo.Clean.Domain;
using Zalo.Clean.Infrastructure.Persistance.DatabaseContext;

namespace Zalo.LeaveManagement.Persistance.IntegrationTests
{
    public class ZaloDatabaseContextTests
    {
        private ZaloDatabaseContext zaloDatabaseContext;

        public ZaloDatabaseContextTests()
        {
            var dbOptions = new DbContextOptionsBuilder<ZaloDatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            zaloDatabaseContext = new ZaloDatabaseContext(dbOptions);
        }

        [Fact]
        public async void Save_SetDataCreatedAndDateModified()
        {
            //Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDay = 10,
                Name = "Test Vacation"
            };

            //Act
            await zaloDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await zaloDatabaseContext.SaveChangesAsync();

            //Assert
            leaveType.DateCreated.ShouldNotBeNull();
            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}