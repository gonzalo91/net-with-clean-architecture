using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Domain;

namespace Zalo.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>()
            {
                new LeaveType{ Id = 1, Name ="LT 1", DefaultDay = 1},
                new LeaveType{ Id = 2, Name ="LT 2", DefaultDay = 5},
                new LeaveType{ Id = 3, Name ="LT 3", DefaultDay = 10},
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();
            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return leaveType;
            });

            return mockRepo;
        }
    }
}
