using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation
{

    public class UpdateLeaveAllocationCommand : IRequest<int>
    {
        public int Id { get; set; }

        public int NumberOfDays { get; set; }

        public int LeaveTypeId { get; set; }

        public int Period { get; set; }
    }

    
}
