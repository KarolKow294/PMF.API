using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMF.API.Entities;
using PMF.API.Models;
using PMF.API.Services;
using System.Security.Claims;

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
        public async Task<ActionResult<List<PartDto>>> GetPartsByOrderIdAsync([FromRoute] int id)
        {
            var partsDtos = await orderService.GetPartsByOrderIdAsync(id);

            return Ok(partsDtos);
        }

        [HttpPost("part")]
        public async Task<ActionResult> CreatePartAsync([FromForm] CreatePartDto newPartDto)
        {
            await orderService.CreatePartAsync(newPartDto);

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePartAsync([FromBody] UpdateOrderDto storageAfterChange, [FromRoute] int id)
        {
            await orderService.UpdatePartAsync(id, storageAfterChange);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePartsAsync([FromBody] int[] partsId)
        {
            await orderService.DeletePartsAsync(partsId);

            return NoContent();
        }
    }
}
