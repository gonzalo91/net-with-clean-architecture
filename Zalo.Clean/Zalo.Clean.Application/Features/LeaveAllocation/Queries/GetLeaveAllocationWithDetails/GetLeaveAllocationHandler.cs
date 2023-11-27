using AutoMapper;
using MediatR;
using Zalo.Clean.Application.Contracts.Persistance;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationWithDetails
{
    public class GetLeaveAllocationHandler : IRequestHandler<GetLeaveAllocationQuery, LeaveAllocationDetailsDto>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;

        public GetLeaveAllocationHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
        }

        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationQuery request, CancellationToken cancellationToken)
        {

            var leaveAllocations = await leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

            var allocation = mapper.Map<LeaveAllocationDetailsDto>(leaveAllocations);

            return allocation;
        }
    }
}
