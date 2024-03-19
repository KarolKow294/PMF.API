using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using PMF.API.Entities;
using PMF.API.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PMF.API.Services
{
    public class OrderService(PmfDbContext dbContext, IMapper mapper, QrCodeService qrService, CsvService csvService) : IOrderService
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

            if (newPartDto.File is not null)
            {
                var file = ConvertFormFileToByteArray(newPartDto.File);
                newPart.Drawing = file;
            }
            dbContext.Part.Add(newPart);
            await dbContext.SaveChangesAsync();

            var qrCodeData = qrService.GenerateQrCodeData(newPart);
            newPart.QrCodeData = qrCodeData;

            await AddStorageToPartAsync(newPart.Id, newPartDto.ActualStorageId, "actual");
            await AddStorageToPartAsync(newPart.Id, newPartDto.DestinationStorageId, "destination");
        }

        public async Task CreateOrdersAsync(IFormFile file)
        {
            var csvPartDtos = csvService.GetDataFromCsv(file);

            var orders = GetOrders(csvPartDtos);
            await dbContext.Order.AddRangeAsync(orders);
            await dbContext.SaveChangesAsync();

            await AddPartsToOrderAsync(csvPartDtos, orders);
        }

        private List<Order> GetOrders(List<CsvPartDto> csvPartDtos)
        {
            var orderNumbers = csvPartDtos
                .OrderBy(p => p.OrderNumber)
                .Select(p => p.OrderNumber)
                .Distinct()
                .ToList();

            var orders = new List<Order>();

            foreach (var orderNumber in orderNumbers)
            {
                var order = new Order()
                {
                    Name = csvPartDtos.FirstOrDefault(p => p.OrderNumber == orderNumber).OrderName,
                    Number = orderNumber
                };
                orders.Add(order);
            }
            return orders;
        }

        private async Task AddPartsToOrderAsync(List<CsvPartDto> csvPartDtos, List<Order> orders)
        {
            var storages = await dbContext.Storage.ToListAsync();

            foreach (var partDto in csvPartDtos)
            {
                var part = mapper.Map<Part>(partDto);
                part.OrderId = orders.SingleOrDefault(o => o.Number == partDto.OrderNumber).Id;

                var qrCodeData = qrService.GenerateQrCodeData(part);
                part.QrCodeData = qrCodeData;

                dbContext.Part.Add(part);
                await dbContext.SaveChangesAsync();

                var actualStorageId = storages.FirstOrDefault(s => s.Name == partDto.ActualStorage).Id;
                await AddStorageToPartAsync(part.Id, actualStorageId, "actual");

                var destinationStorageId = storages.FirstOrDefault(s => s.Name == partDto.DestinationStorage).Id;
                await AddStorageToPartAsync(part.Id, destinationStorageId, "destination");
            }
        }

        private async Task AddStorageToPartAsync(int partId, int storageId, string type)
        {
            var partStorage = CreatePartStorage(partId, storageId, type);
            dbContext.PartStorage.Add(partStorage);
            await dbContext.SaveChangesAsync();
        }

        private byte[] ConvertFormFileToByteArray(IFormFile formFile)
        {
            using var fileStream = formFile.OpenReadStream();
            byte[] file = new byte[formFile.Length];
            fileStream.Read(file, 0, (int)file.Length);

            return file;
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

        public async Task UpdateDrawingAsync(int partId, IFormFile drawing)
        {
            var convertedDrawing = ConvertFormFileToByteArray(drawing);
            var part = await dbContext.Part.FirstOrDefaultAsync(p => p.Id == partId);
            part.Drawing = convertedDrawing;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await dbContext.Order.FirstOrDefaultAsync(o => o.Id == id);
            dbContext.Order.Remove(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeletePartsAsync(int[] partsId)
        {
            var partsToDelete = await dbContext
                .Part.Where(t => partsId.Contains(t.Id)).ToListAsync();

            dbContext.Part.RemoveRange(partsToDelete);
            await dbContext.SaveChangesAsync();
        }
    }
}
