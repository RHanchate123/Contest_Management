using Contest_Management.API.Seeders;
using Contest_Management.Model;
using IdentityApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace Contest_Management.Seeders
{
    public class Seeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CMSDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            await Roles.SeedRolesAsync(context, roleManager, userManager, configuration);
            await AddContest.SeedContestAsync(context);
        }

    }
}
