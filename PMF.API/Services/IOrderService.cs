using PMF.API.Models;

namespace PMF.API.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();
        Task<List<PartDto>> GetByIdAsync(int orderId);
        Task UpdateAsync(int partId, UpdateOrderDto storageAfterChange);
    }
}
