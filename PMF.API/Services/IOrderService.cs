using PMF.API.Models;

namespace PMF.API.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();
        Task<List<PartDto>> GetPartsByOrderIdAsync(int orderId);
        Task CreatePartAsync(CreatePartDto newPartDto);
        Task UpdatePartAsync(int partId, UpdateOrderDto storageAfterChange);
        Task DeletePartsAsync(int[] partsId);
    }
}
