using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zalo.Clean.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommand:IRequest<bool>
    {
        public int Id { get; set; }
        public bool Approved { get; set; }
    }
}
