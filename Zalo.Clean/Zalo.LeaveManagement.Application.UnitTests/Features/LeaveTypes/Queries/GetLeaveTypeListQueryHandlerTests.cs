using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Logging;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Zalo.Clean.Application.MappingProfiles;
using Zalo.LeaveManagement.Application.UnitTests.Mocks;

namespace Zalo.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeListQueryHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> mockRepo;
        private IMapper mockMapper;
        private Mock<IAppLogger<GetLeaveTypesQueryHandler>> mockAppLogger;
        public GetLeaveTypeListQueryHandlerTests()
        {
            mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            mockMapper = mapperConfig.CreateMapper();
            mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
        }

        [Fact]
        public async Task GetLeaveTypeListTest()
        {

            var handler = new GetLeaveTypesQueryHandler(mockMapper, mockRepo.Object, mockAppLogger.Object);

            var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();

            mockAppLogger.Verify(d=>d.info("Leave types were retrieved succesfully"));            

            result.Count.ShouldBe(3);

            Assert.Equal(3, result.Count);

        }
    }
}
