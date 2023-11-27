using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Domain;

namespace Zalo.Clean.Infrastructure.Persistance.DatabaseContext.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(ZaloDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await context.AddRangeAsync(allocations);
            await context.SaveChangesAsync();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
        {
            var leaveAllocations = await context.LeaveAllocations
                                .Include(q => q.LeaveType)
                                .Where(q => q.UserId == userId)
                                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocations = await context.LeaveAllocations
                                .Include(q => q.LeaveType)
                                .FirstOrDefaultAsync(q => q.Id == id);

            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = await context.LeaveAllocations
                                .Include(q => q.LeaveType)
                                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetUserAllocation(string userId, int leaveTypeId)
        {
            return await context.LeaveAllocations.FirstOrDefaultAsync(q=>q.UserId == userId && q.LeaveTypeId == leaveTypeId);
        }

        public async Task<bool> HasLeaveAllocationAsync(string userId, int leaveTypeId, int period)
        {
            return await context.LeaveAllocations.AnyAsync(q => q.Period == period && q.LeaveTypeId == leaveTypeId
                                                            && q.UserId == userId);
        }
    }
}
