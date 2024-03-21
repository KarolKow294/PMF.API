using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMF.API.Models;
using PMF.API.Services;

namespace PMF.API.Controllers
{
    [Route("api/storages")]
    [ApiController]
    [Authorize]
    public class StorageController(IStorageService storageService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAllAsync()
        {
            var storages = await storageService.GetAllAsync();

            return Ok(storages);
        }
    }
}
