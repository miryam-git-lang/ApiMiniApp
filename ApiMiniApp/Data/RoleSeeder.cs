using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ApiMiniApp.Data;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["admin", "member"];

        foreach (var role in roles)
        {
            if (await roleManager.RoleExistsAsync(role))
            {
                continue;
            }

            var result = await roleManager.CreateAsync(new IdentityRole(role));
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Failed to create role '{role}': {errors}");
            }
        }
    }
}
