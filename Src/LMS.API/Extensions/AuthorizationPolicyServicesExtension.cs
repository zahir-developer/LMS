using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LMS.API.Extensions;

public static class AuthorizationPolicyServicesExtension
{
    public static IServiceCollection AddAuthorizationPolicyServices(this IServiceCollection services)
    {
        services.AddAuthorization(x=>
        {
            x.AddPolicy("User_Signup", policy=>policy.RequireRole("Admin"));
            x.AddPolicy("User_View_All", policy=>policy.RequireRole("Admin"));
            x.AddPolicy("User_Edit_Update", policy=>policy.RequireRole("Admin"));
            x.AddPolicy("Leave_View_All", policy=>policy.RequireRole("Admin"));
            x.AddPolicy("Leave_Approve_Reject", policy=>policy.RequireRole("Admin"));

            x.AddPolicy("Leave_View", policy=>policy.RequireRole("User"));
            x.AddPolicy("Leave_Apply", policy=>policy.RequireRole("User"));           
        });

        return services;

    }
}
