using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Command.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommand : IRequest<int>
    {
        public int LeaveTypeId { get; set; }
    }
}
