using Microsoft.AspNetCore.Mvc;
using PMF.API.Models;
using PMF.API.Services;

namespace PMF.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var ordersDtos = orderService.GetAll();

            return Ok(ordersDtos);
        }
    }
}
