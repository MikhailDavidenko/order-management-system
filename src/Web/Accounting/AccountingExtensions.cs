using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using OrderManagementSystem.Data.Common;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Web.Accounting;

public static class AccountingExtensions
{
    /// <summary>
    /// Регистрирует Identity сервисы
    /// </summary>
    public static IServiceCollection ConfigureIdentity(
        this IServiceCollection services)
    {
        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    },
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }
                };
            })
            .AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorization(options =>
        {
            options.AddPolicy(ApplicationRoleNames.AllowAnyPolicy,
                policy => policy.RequireRole(ApplicationRoleNames.AllRoles));
        });

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole<Guid>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }
    
    public static async Task InitializeRolesAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    
        var roleNames = ApplicationRoleNames.AllRoles;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }
    }
}