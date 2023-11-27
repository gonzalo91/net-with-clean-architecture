using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Features.LeaveRequest.GetLeaveRequestList;

namespace Zalo.Clean.Application.Features.LeaveRequest.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQuery : IRequest<GetLeaveRequestDetailDto>
    {
        public int Id { get; set; }
    }
}
