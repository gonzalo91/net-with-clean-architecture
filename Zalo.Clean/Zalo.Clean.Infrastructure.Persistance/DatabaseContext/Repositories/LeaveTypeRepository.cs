using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Domain;

namespace Zalo.Clean.Infrastructure.Persistance.DatabaseContext.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(ZaloDatabaseContext context) : base(context)
        {
        }

        public async Task<bool> IsUniqueNameAsync(string name)
        {
            return (await context.LeaveTypes.AnyAsync(q => q.Name == name)) == false;
        }
    }
}
