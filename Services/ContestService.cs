using Contest_Management.API.Interfaces;
using Contest_Management.Entities;
using Contest_Management.Interfaces;
using ContestSystem.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Contest_Management.Services
{
    public class ContestService : IContestInterface
    {
        private readonly ContestRepository _repository;
        public ContestService(ContestRepository repository)
        {
            _repository = repository;
        }

        [EnableRateLimiting("readLimiter")]
        [HttpGet]
        public async Task<List<Contest>> GetAllAsync()
        {
           return await _repository.GetAllAsync();
        }
    }
}
