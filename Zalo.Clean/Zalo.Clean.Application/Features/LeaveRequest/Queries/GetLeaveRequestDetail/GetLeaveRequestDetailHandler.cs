using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Features.LeaveRequest.GetLeaveRequestList;

namespace Zalo.Clean.Application.Features.LeaveRequest.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailHandler : IRequestHandler<GetLeaveRequestDetailQuery, GetLeaveRequestDetailDto>
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public GetLeaveRequestDetailHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            this.mapper = mapper;
            this.leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<GetLeaveRequestDetailDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest = mapper.Map<GetLeaveRequestDetailDto>(
                await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));

            return leaveRequest;
        }
    }
}
