using FluentValidation;
using FluentValidation.AspNetCore;
using LMS.Application.DTOs;
using LMS.Application.FluentValidators;

namespace LMS.API.Extensions;

public static class FluentValidationServicesExtension
{
    public static IServiceCollection AddFluentValidationServicesExtension(this IServiceCollection services)
    {
        services.AddScoped<IValidator<LeaveStatusUpdateDto>, LeaveStatusUpdateValidator>();
        services.AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters()
        .AddValidatorsFromAssemblyContaining<LeaveStatusUpdateValidator>();
        

        // Load using a type reference rather than the generic.
        //services.AddValidatorsFromAssemblyContaining(typeof(LeaveStatusUpdateValidator));
            
        //services.AddValidatorsFromAssemblyContaining<LeaveStatusUpdateValidator>(ServiceLifetime.Transient);
        return services;

    }
}
