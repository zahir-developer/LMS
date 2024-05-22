using LMS.Infrastructure.Repositories;
using LMS.Infrastructure.Database;
using LMS.Application.Interfaces;
using LMS.Application.Services;
using LMS.Application.ServiceMappings;
using LMS.Application.Interfaces.ServiceMappings;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Mappings;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            services.AddDbContext<LMSDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("SqliteConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //AutoMapper Configuration
            services.AddAutoMapper(typeof(MappingProfile));

            //Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Generic Services
            services.AddScoped(typeof(IReadServiceAsync<,>), typeof(ReadServiceAsync<,>));

            // Services
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped<IAuthTokenService, AuthTokenService>();
            services.AddScoped(typeof(ILeaveTypeService), typeof(LeaveTypeService));
            services.AddScoped(typeof(IUserLeaveService), typeof(UserLeaveService));

            return services;
        }
    }
}