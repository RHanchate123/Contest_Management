using Contest_Management.Model;
using IdentityApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace Contest_Management.API.Seeders
{
    public static class Roles
    {
        public static async Task SeedRolesAsync(CMSDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration _config)
        {
            string[] roleNames = { "Admin", "VIP", "Signed-in", "Guest" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

    }

}

