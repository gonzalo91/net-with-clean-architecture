﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zalo.Clean.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class LeaveTypeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultDay { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set;}

    }
}
