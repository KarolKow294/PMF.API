using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMF.API.Entities;
using PMF.API.Models;

namespace PMF.API.Services
{
    public class StorageService(PmfDbContext dbContext) : IStorageService
    {
        public async Task<List<Storage>> GetAllAsync()
        {
            var storages = await dbContext
                .Storage
                .ToListAsync();

            return storages;
        }

        public async Task<string> GetActualStorageNameAsync(int partId)
        {            
            var partStorage = await dbContext
                .PartStorage
                .FirstOrDefaultAsync(p => p.PartId == partId && p.Type == "actual");

            var storage = await dbContext
                .Storage
                .FirstOrDefaultAsync(s => s.Id == partStorage.StorageId);

            return storage.Name;
        }
    }
}
