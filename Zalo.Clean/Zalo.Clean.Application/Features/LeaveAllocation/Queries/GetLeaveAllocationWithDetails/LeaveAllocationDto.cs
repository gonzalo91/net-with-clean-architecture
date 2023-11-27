using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationWithDetails
{
    public class LeaveAllocationDetailsDto
    {

        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public int leaveTypeId { get; set; }
        public int Period { get; set; }

        public LeaveTypeDto leaveTypeDto { get; set; }

    }
}
