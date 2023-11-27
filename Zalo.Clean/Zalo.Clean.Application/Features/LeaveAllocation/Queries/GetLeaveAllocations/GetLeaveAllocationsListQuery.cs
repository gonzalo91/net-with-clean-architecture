using MediatR;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationsListQuery : IRequest<List<LeaveAllocationDto>>
    {

    }
}
