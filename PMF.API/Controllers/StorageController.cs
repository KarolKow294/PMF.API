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
        public ActionResult<List<OrderDto>> GetAll()
        {
            var storages = storageService.GetAll();

            return Ok(storages);
        }
    }
}
