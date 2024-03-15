using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMF.API.Entities;
using PMF.API.Models;

namespace PMF.API.Services
{
    public class StorageService(PmfDbContext dbContext, IMapper mapper) : IStorageService
    {
        public async Task<List<Storage>> GetAllAsync()
        {
            var storages = await dbContext
            .Storage
            .ToListAsync();

            return storages;
        }
    }
}
