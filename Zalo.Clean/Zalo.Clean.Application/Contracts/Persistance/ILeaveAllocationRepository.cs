using Zalo.Clean.Domain;

namespace Zalo.Clean.Application.Contracts.Persistance;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);


    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();

    Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId);

    Task<bool> HasLeaveAllocationAsync(string userId, int leaveTypeId, int period);

    Task AddAllocations(List<LeaveAllocation> allocations);

    Task <LeaveAllocation> GetUserAllocation(string userId, int leaveTypeId);
}

