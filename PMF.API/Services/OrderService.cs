using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using PMF.API.Entities;
using PMF.API.Models;

namespace PMF.API.Services
{
    public class OrderService(PmfDbContext dbContext, IMapper mapper) : IOrderService
    {
        public async Task<List<OrderDto>> GetAllAsync()
        {
            var parts = await dbContext
            .Part
            .Include(r => r.Storages)
            .ToListAsync();

            var partDtos = mapper.Map<List<PartDto>>(parts);

            var orders = await dbContext
            .Order
            .Include(r => r.Parts)
            .ToListAsync();

            var orderDtos = mapper.Map<List<OrderDto>>(orders);

            return orderDtos;
        }

        public async Task<List<PartDto>> GetByIdAsync(int orderId)
        {
            var parts = await dbContext
                .Part
                .Include(r => r.Storages)
                .Where(r => r.OrderId == orderId)
                .ToListAsync();

            var partDtos = mapper.Map<List<PartDto>>(parts);

            return partDtos;
        }

        public async Task UpdateAsync(int partId, UpdateOrderDto storageAfterChange)
        {
            var newPartStorage = new PartStorage()
            {
                PartId = partId,
                StorageId = storageAfterChange.Id,
                Type = storageAfterChange.Type,
            };

            var oldStoragePart = await dbContext
                .PartStorage
                .FirstOrDefaultAsync(r => r.PartId == partId && r.Type == storageAfterChange.Type);

            if (oldStoragePart == null)
                throw new Exception("PartStorage not found");

            dbContext.PartStorage.Add(newPartStorage);
            dbContext.PartStorage.Remove(oldStoragePart);
            await dbContext.SaveChangesAsync();
        }
    }
}
