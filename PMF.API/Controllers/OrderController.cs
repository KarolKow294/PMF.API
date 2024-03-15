using Microsoft.AspNetCore.Mvc;
using PMF.API.Entities;
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

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateOrderDto storageAfterChange, [FromRoute] int id)
        {
            orderService.Update(id, storageAfterChange);

            return Ok();
        }
    }
}
