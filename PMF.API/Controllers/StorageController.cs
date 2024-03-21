using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMF.API.Models;
using PMF.API.Services;

namespace PMF.API.Controllers
{
    [Route("api/storages")]
    [ApiController]
    public class StorageController(IStorageService storageService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAllAsync()
        {
            var storages = await storageService.GetAllAsync();

            return Ok(storages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetActualStorageNameAsync([FromRoute] int id)
        {
            var storageName = await storageService.GetActualStorageNameAsync(id);

            return Ok(storageName);
        }
    }
}
