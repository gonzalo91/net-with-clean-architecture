using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

namespace Zalo.Clean.Application.Features.LeaveRequest.GetLeaveRequestList
{
    public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDto>>
    {
    }
}
