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
        public List<OrderDto> GetAll()
        {
            var parts = dbContext
            .Part
            .Include(r => r.Storages)
            .ToList();

            var partDtos = mapper.Map<List<PartDto>>(parts);

            var orders = dbContext
            .Order
            .Include(r => r.Parts)
            .ToList();

            var orderDtos = mapper.Map<List<OrderDto>>(orders);

            return orderDtos;
        }

        public void Update(int partId, UpdateOrderDto storageAfterChange)
        {
            var newPartStorage = new PartStorage()
            {
                PartId = partId,
                StorageId = storageAfterChange.Id,
                Type = storageAfterChange.Type,
            };

            var oldStoragePart = dbContext
                .PartStorage
                .FirstOrDefault(r => r.PartId == partId && r.Type == storageAfterChange.Type);

            if (oldStoragePart == null)
                throw new Exception("PartStorage not found");

            dbContext.PartStorage.Add(newPartStorage);
            dbContext.PartStorage.Remove(oldStoragePart);
            dbContext.SaveChanges();
        }
    }
}
