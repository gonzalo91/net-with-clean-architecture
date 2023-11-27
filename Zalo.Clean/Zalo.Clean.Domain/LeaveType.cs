using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Domain.Common;

namespace Zalo.Clean.Domain;

public class LeaveType : BaseEntity
{    
    public string Name { get; set; } = string.Empty;
    public int DefaultDay { get; set; }
}


