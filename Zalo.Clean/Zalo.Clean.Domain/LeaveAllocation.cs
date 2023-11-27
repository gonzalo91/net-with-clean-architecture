using Zalo.Clean.Domain.Common;

namespace Zalo.Clean.Domain
{
    public class LeaveAllocation : BaseEntity
    {        
        public string UserId { get; set; } = string.Empty;
        public int NumberOfDays { get; set; }

        public LeaveType LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public int Period { get; set; }
    }


}
