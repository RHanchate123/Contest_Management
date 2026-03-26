using Contest_Management.Entities;

namespace Contest_Management.Interfaces
{
    public interface IContestInterface
    {
        public Task<List<Contest>> GetAllAsync();
    }
}
