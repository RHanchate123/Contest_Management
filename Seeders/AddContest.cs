using Contest_Management.Entities;
using Contest_Management.Model;
using IdentityApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace Contest_Management.Seeders
{

    public static class AddContest
    {
        public static async Task SeedContestAsync(CMSDbContext dbContext)
        {
            if (dbContext.CONTESTS.Any())
                return; // already seeded

            var contests = new List<Contest>
        {
            new Contest
            {
                Name = "General Knowledge Blast",
                Description = "Test your everyday knowledge across multiple domains.",
                accessLevel = AccessLevel.Normal,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(7),
                Prize = "₹5,000 Amazon Voucher"
            },
            new Contest
            {
                Name = "VIP Coding Challenge",
                Description = "Exclusive contest for VIP users with advanced coding questions.",
                accessLevel = AccessLevel.VIP,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(5),
                Prize = "₹25,000 Cash Reward"
            },
            new Contest
            {
                Name = "Quick Trivia Showdown",
                Description = "Fast-paced trivia with single, multi-select and true/false questions.",
                accessLevel = AccessLevel.Normal,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(3),
                Prize = "₹2,000 Gift Card"
            }
        };

            await dbContext.CONTESTS.AddRangeAsync(contests);
            await dbContext.SaveChangesAsync();
        }
    }
}
