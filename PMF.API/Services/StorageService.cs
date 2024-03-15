using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMF.API.Entities;
using PMF.API.Models;

namespace PMF.API.Services
{
    public class StorageService(PmfDbContext dbContext, IMapper mapper) : IStorageService
    {
        public List<Storage> GetAll()
        {
            var storages = dbContext
            .Storage
            .ToList();

            return storages;
        }
    }
}
