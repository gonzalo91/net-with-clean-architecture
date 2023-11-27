using Zalo.Clean.Domain;

namespace Zalo.Clean.Application.Contracts.Persistance;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsUniqueNameAsync(string name);
}

