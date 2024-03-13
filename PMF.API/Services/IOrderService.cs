using PMF.API.Models;

namespace PMF.API.Services
{
    public interface IOrderService
    {
        List<OrderDto> GetAll();
    }
}
