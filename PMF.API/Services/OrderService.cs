using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using PMF.API.Entities;
using PMF.API.Models;

namespace PMF.API.Services
{
    public class OrderService(PmfDbContext dbContext, IMapper mapper, QrCodeService qrService) : IOrderService
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

        public async Task<List<PartDto>> GetPartsByOrderIdAsync(int orderId)
        {
            var parts = await dbContext
                .Part
                .Include(r => r.Storages)
                .Where(r => r.OrderId == orderId)
                .ToListAsync();

            var partDtos = mapper.Map<List<PartDto>>(parts);

            return partDtos;
        }

        public async Task CreatePartAsync(CreatePartDto newPartDto)
        {
            var newPart = mapper.Map<Part>(newPartDto);
            dbContext.Part.Add(newPart);
            await dbContext.SaveChangesAsync();

            var qrCodeData = qrService.GenerateQrCodeData(newPart);
            newPart.QrCodeData = qrCodeData;

            var partStorages = await dbContext
                .PartStorage
                .ToListAsync();

            var actualPartStorage = CreatePartStorage(newPart.Id, newPartDto.ActualStorageId, "actual");
            partStorages.Add(actualPartStorage);

            var destinationPartStorage = CreatePartStorage(newPart.Id, newPartDto.DestinationStorageId, "destination");
            partStorages.Add(destinationPartStorage);

            await dbContext.SaveChangesAsync();
        }

        private PartStorage CreatePartStorage(int partId, int storageId, string type)
        {
            var partStorage = new PartStorage()
            {
                PartId = partId,
                StorageId = storageId,
                Type = type
            };
            return partStorage;
        }

        public async Task UpdatePartAsync(int partId, UpdateOrderDto storageAfterChange)
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

        public async Task DeletePartsAsync(int[] partsId)
        {
            var partsToDelete = dbContext
                .Part.Where(t => partsId.Contains(t.Id));

            dbContext.RemoveRange(partsToDelete);
            await dbContext.SaveChangesAsync();
        }
    }
}
