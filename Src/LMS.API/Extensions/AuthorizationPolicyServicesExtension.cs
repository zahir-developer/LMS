using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LMS.API.Extensions;

public static class AuthorizationPolicyServicesExtension
{
    public static IServiceCollection AddAuthorizationPolicyServices(this IServiceCollection services)
    {
        services.AddAuthorization(x=>
        {
            //Module: Users
            //Role: Admin
            x.AddPolicy("User_Signup", policy=>policy.RequireRole("Admin"));
            x.AddPolicy("User_View_All", policy=>policy.RequireRole("Admin"));
            x.AddPolicy("User_Edit_Update", policy=>policy.RequireRole("Admin"));
            x.AddPolicy("User_Delete", policy=>policy.RequireRole("Admin"));
            x.AddPolicy("User_Email_Exists", policy=>policy.RequireRole("Admin"));
            

            //Module: Leave
            //Role: Admin
            x.AddPolicy("Leave_View_All", policy=>policy.RequireRole("Admin"));
            x.AddPolicy("Leave_Approve_Reject", policy=>policy.RequireRole("Admin"));
            //Role: User
            x.AddPolicy("Leave_View", policy=>policy.RequireRole("User"));
            x.AddPolicy("Leave_Apply", policy=>policy.RequireRole("User"));          
        });

        return services;

    }
}
