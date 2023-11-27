using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace Zalo.Clean.Application.Features.LeaveRequest.GetLeaveRequestList
{
    public class LeaveRequestListDto
    {

        public string RequestingEmployeeId { get; set; }

        public LeaveTypeDto LeaveType { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool? Approved { get; set; }  

    }
}
