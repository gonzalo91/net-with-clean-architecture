using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class LeaveAllocationDto
    {

        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public int leaveTypeId { get; set; }

        public int Period { get; set; }

    }
}
