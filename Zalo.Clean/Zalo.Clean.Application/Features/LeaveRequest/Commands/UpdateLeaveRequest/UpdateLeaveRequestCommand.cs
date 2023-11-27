using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Features.LeaveRequest.Shared;

namespace Zalo.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest<int>
    {
        public int Id { get; set; }

        public string RequestComments { get; set; } = string.Empty;

        public bool Cancelled { get; set; } = false;
    }
}
