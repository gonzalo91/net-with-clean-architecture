using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zalo.LeaveManagement.Identity.Models;

namespace Zalo.LeaveManagement.Identity.DbContext
{
    public class ZaloLeaveManagmentIdentityDbContext: IdentityDbContext<ApplicationUser>
    {

        public ZaloLeaveManagmentIdentityDbContext(
            DbContextOptions<ZaloLeaveManagmentIdentityDbContext> options) : base( options ) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ZaloLeaveManagmentIdentityDbContext).Assembly);
        }


    }
}
