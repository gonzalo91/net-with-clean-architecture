using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

namespace Zalo.Clean.Application.Features.LeaveRequest.GetLeaveRequestList
{
    public class GetLeaveRequestListHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public GetLeaveRequestListHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            this.mapper = mapper;
            this.leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            var leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails();

            var requests = mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

            return requests;
        }
    }
}
