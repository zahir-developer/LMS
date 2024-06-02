using Microsoft.CodeAnalysis.CSharp.Syntax;
using static LMS.Application.Constants.ConstEnum;

namespace LMS.API.Extensions;

public static class AuthorizationPolicyServicesExtension
{
    public static IServiceCollection AddAuthorizationPolicyServices(this IServiceCollection services)
    {
        services.AddAuthorization(x=>
        {   
            //Module: Users
            //Role: Admin
            x.AddPolicy("User_Signup", policy=>policy.RequireRole(Roles.Admin.ToString()));
            x.AddPolicy("User_View_All", policy=>policy.RequireRole(Roles.Admin.ToString()));
            x.AddPolicy("User_Edit_Update", policy=>policy.RequireRole(Roles.Admin.ToString()));
            x.AddPolicy("User_Delete", policy=>policy.RequireRole(Roles.Admin.ToString()));
            x.AddPolicy("User_Email_Exists", policy=>policy.RequireRole(Roles.Admin.ToString()));
            

            //Module: Leave
            //Role: Admin
            x.AddPolicy("Leave_View_All", policy=>policy.RequireRole(Roles.Manager.ToString()));
            x.AddPolicy("Leave_Approve_Reject", policy=>policy.RequireRole(Roles.Manager.ToString()));
            x.AddPolicy("LeaveReport", policy=>policy.RequireRole(Roles.Manager.ToString()));
            //Role: User
            x.AddPolicy("Leave_View", policy=>policy.RequireRole(Roles.Employee.ToString()));
            x.AddPolicy("Leave_Apply", policy=>policy.RequireRole(Roles.Employee.ToString()));          
        });

        return services;

    }
}
