
using LMS.Infrastructure.Database;
using LMS.Infrastructure.Repository;
using LMS.Application.Interfaces;
using LMS.Application.Services;
using LMS.Application.ServiceMappings;
using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IRepository;
using LMS.Application.AutoMapper;
using LMS.Application.IServiceMappings;
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
            services.AddAutoMapper(typeof(AutoMappingProfile));

            //Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Generic Services
            services.AddScoped(typeof(IReadServiceAsync<,>), typeof(ReadServiceAsync<,>));

            // Services
            services.AddScoped(typeof(IUserServiceMapping), typeof(UserServiceMapping));
            services.AddScoped<IAuthTokenService, AuthTokenService>();
            services.AddScoped(typeof(ILeaveTypeService), typeof(LeaveTypeService));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IUserLeaveServiceMapping), typeof(UserLeaveServiceMapping));
            services.AddScoped(typeof(IUserLeaveRepository), typeof(UserLeaveRepository));            
            //services.AddScoped(typeof(IRoleServiceMapping), typeof(RoleServiceMapping));
            
            return services;
        }
    }
}