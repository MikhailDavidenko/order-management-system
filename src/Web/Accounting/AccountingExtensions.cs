using Microsoft.AspNetCore.Identity;

namespace OrderManagementSystem.Web.Accounting;

public static class AccountingExtensions
{
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