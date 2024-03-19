using PMF.API.Models;

namespace PMF.API.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();
        Task<List<PartDto>> GetPartsByOrderIdAsync(int orderId);
        Task CreatePartAsync(CreatePartDto newPartDto);
        Task CreateOrdersAsync(IFormFile file);
        Task UpdatePartAsync(int partId, UpdateOrderDto storageAfterChange);
        Task DeleteOrderAsync(int id);
        Task DeletePartsAsync(int[] partsId);
    }
}
