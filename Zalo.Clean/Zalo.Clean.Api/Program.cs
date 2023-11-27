using Zalo.Clean.Api.Middlewares;
using Zalo.Clean.Application;
using Zalo.Clean.Infrastructure;
using Zalo.Clean.Infrastructure.Persistance;
using Zalo.LeaveManagement.Identity;

namespace Zalo.Clean.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistanceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("all", builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();


            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            if(app.Environment.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();                
            }
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            
            app.Run();
        }
    }
}