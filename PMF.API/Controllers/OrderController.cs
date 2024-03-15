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
        public async Task<ActionResult<List<OrderDto>>> GetAllAsync()
        {
            var ordersDtos = await orderService.GetAllAsync();

            return Ok(ordersDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<PartDto>>> GetByIdAsync([FromRoute] int id)
        {
            var partsDtos = await orderService.GetByIdAsync(id);

            return Ok(partsDtos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateOrderDto storageAfterChange, [FromRoute] int id)
        {
            await orderService.UpdateAsync(id, storageAfterChange);

            return Ok();
        }
    }
}
