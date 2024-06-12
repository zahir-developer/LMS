
using LMS.Infrastructure.Database;
using LMS.Infrastructure.Repository;
using LMS.Application.Interfaces;
using LMS.Application.Services;
using LMS.Application.ServiceMappings;
using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IRepository;
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
            options.UseSqlite(config.GetConnectionString("SqliteConnection"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            /*services.AddDbContext<LMSDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnectionString"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));*/

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
            //services.AddAutoMapper(typeof(AutoMappingProfile));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericServiceAsync<,>), typeof(GenericServiceAsync<,>));

            // Services            
            services.AddScoped(typeof(IUserServiceMapping), typeof(UserServiceMapping));
            services.AddScoped<IAuthTokenService, AuthTokenService>();
            services.AddScoped(typeof(ILeaveTypeService), typeof(LeaveTypeService));
            //services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            //services.AddScoped(typeof(IUserLeaveRepository), typeof(UserLeaveRepository));  
            services.AddScoped(typeof(IUserLeaveServiceMapping), typeof(UserLeaveServiceMapping));
            services.AddScoped(typeof(IDepartmentServiceMapping), typeof(DepartmentServiceMapping));
            services.AddScoped(typeof(IRoleService), typeof(RoleService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
                 
            //services.AddScoped(typeof(IRoleServiceMapping), typeof(RoleServiceMapping));
                    
            return services;
        }
    }
}