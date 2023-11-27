using Microsoft.Extensions.DependencyInjection;
using Zalo.Clean.Infrastructure.Persistance.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Infrastructure.Persistance.DatabaseContext.Repositories;

namespace Zalo.Clean.Infrastructure.Persistance
{
    public static class PersistanceServiceRegistration
    {

        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<ZaloDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ZaloDatabaseConnectionString"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();


            return services;
        }
                

    }
}