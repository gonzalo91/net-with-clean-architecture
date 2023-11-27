using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zalo.Clean.Application.Contracts.Email;
using Zalo.Clean.Application.Contracts.Logging;
using Zalo.Clean.Application.Modules.Email;
using Zalo.Clean.Infrastructure.EmailService;
using Zalo.Clean.Infrastructure.Logging;

namespace Zalo.Clean.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}