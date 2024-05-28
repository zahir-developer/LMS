using FluentValidation;
using FluentValidation.AspNetCore;
using LMS.Application.DTOs;
using LMS.Application.FluentValidators;

namespace LMS.API.Extensions;

public static class FluentValidationServicesExtension
{
    public static IServiceCollection AddFluentValidationServicesExtension(this IServiceCollection services)
    {
        // services.AddFluentValidationAutoValidation()
        // .AddFluentValidationClientsideAdapters().AddValidatorsFromAssemblyContaining<LeaveStatusUpdateValidator>();
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddScoped<IValidator<LeaveStatusUpdateDto>, LeaveStatusUpdateValidator>();
        
        return services;

    }
}
