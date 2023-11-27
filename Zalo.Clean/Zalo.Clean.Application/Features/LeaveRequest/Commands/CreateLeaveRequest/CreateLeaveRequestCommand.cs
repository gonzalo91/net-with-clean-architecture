using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Features.LeaveRequest.Shared;

namespace Zalo.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommand: BaseLeaveRequest, IRequest<int>
    {
        public string RequestComments { get; set; } = string.Empty;
    }
}
