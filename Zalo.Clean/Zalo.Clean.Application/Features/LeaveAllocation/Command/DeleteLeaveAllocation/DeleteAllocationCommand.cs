using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Command.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommand: IRequest<int>
    {
        public int Id { get; set; }
    }
}
