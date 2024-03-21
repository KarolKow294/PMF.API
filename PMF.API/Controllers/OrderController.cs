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

        [HttpPost("csv")]
        public async Task<ActionResult> CreateOrdersAsync([FromForm] IFormFile file)
        {
            if (file is null)
                return BadRequest("File is empty");

            await orderService.CreateOrdersAsync(file);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePartAsync([FromBody] UpdateOrderDto storageAfterChange, [FromRoute] int id)
        {
            await orderService.UpdatePartAsync(id, storageAfterChange);

            return Ok();
        }

        [HttpPut("part/drawing/{id}")]
        public async Task<ActionResult> UpdateDrawingAsync([FromForm] IFormFile drawing, [FromRoute] int id)
        {
            if (drawing is null)
                return BadRequest("File is empty");

            await orderService.UpdateDrawingAsync(id, drawing);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderAsync([FromRoute] int id)
        {
            await orderService.DeleteOrderAsync(id);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePartsAsync([FromBody] int[] partsId)
        {
            await orderService.DeletePartsAsync(partsId);

            return NoContent();
        }
    }
}
