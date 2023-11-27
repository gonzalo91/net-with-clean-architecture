using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Domain;
using Zalo.Clean.Domain.Common;

namespace Zalo.Clean.Infrastructure.Persistance.DatabaseContext
{
    public class ZaloDatabaseContext : DbContext
    {
        public ZaloDatabaseContext(DbContextOptions<ZaloDatabaseContext> options) : base(options)
        {
            

        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set;}
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZaloDatabaseContext).Assembly);

            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType { Id = 1, Name = "Vacation", DefaultDay = 10, DateCreated = DateTime.Now , DateModified = DateTime.MinValue }
            );
            
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var item in base.ChangeTracker.Entries<BaseEntity>()
                                        .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified)){

                item.Entity.DateModified = DateTime.Now;

                if(item.State == EntityState.Added)
                {
                    item.Entity.DateCreated = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
