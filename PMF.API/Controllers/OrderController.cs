using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMF.API.Entities;

namespace PMF.API.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Part>> GetAll()
        {
            //var restaurantsDtos = _restaurantService.GetAll(query);

            //return Ok(restaurantsDtos);

            return Ok("wszystko ok");
        }
    }
}
