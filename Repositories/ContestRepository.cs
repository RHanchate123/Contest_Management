using Contest_Management.Entities;
using Contest_Management.Model;
using Microsoft.EntityFrameworkCore;

namespace ContestSystem.API.Repositories;

public class ContestRepository
{
    private readonly CMSDbContext _context;
    public ContestRepository(CMSDbContext context)
    {
        _context = context;
    }

    public async Task<List<Contest>> GetAllAsync()
    {
        return await _context.CONTESTS.ToListAsync();
    }

}