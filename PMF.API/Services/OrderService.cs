using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    }
}
