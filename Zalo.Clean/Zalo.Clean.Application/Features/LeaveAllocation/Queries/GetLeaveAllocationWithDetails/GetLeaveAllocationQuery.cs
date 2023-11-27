using MediatR;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationWithDetails
{
    public class GetLeaveAllocationQuery : IRequest<LeaveAllocationDetailsDto>
    {
        public int Id { get; set; }
    }
}
