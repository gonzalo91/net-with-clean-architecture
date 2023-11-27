using AutoMapper;
using MediatR;
using Zalo.Clean.Application.Contracts.Persistance;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationsListHandler : IRequestHandler<GetLeaveAllocationsListQuery, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;

        public GetLeaveAllocationsListHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsListQuery request, CancellationToken cancellationToken)
        {

            var leaveAllocations = await leaveAllocationRepository.GetLeaveAllocationsWithDetails();

            var allocations = mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

            return allocations;

        }
    }
}
