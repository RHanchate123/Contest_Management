using Contest_Management.Entities;
using IdentityApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contest_Management.Model
{
    public class CMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public CMSDbContext(DbContextOptions<CMSDbContext> options) : base(options)
        {

        }

        //Define all the required tables here. 

        public DbSet<Contest> CONTESTS { get; set; }
        public DbSet<Questions> QUESTIONS { get; set; }
        public DbSet<LeaderBoard> LEADERBOARD { get; set; } 

    }
}
