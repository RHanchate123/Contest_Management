using Contest_Management.Model;

namespace ContestSystem.API.Repositories;

public class UserRepository
{
    private readonly CMSDbContext _dbContext;
    public UserRepository( CMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

}