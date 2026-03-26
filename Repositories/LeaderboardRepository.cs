using Contest_Management.Model;

namespace ContestSystem.API.Repositories;

public class LeaderboardRepository
{
    private readonly CMSDbContext _context;
    public LeaderboardRepository(CMSDbContext context)
    {
        _context = context;
    }

}