using Microsoft.AspNetCore.Mvc;
using PMF.API.Models;
using PMF.API.Services;

namespace PMF.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto user)
        {
            await accountService.RegisterUser(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task <ActionResult> Login([FromBody] LoginDto user)
        {
            //string token = accountService.GenerateJwt(user);
            return Ok();
        }
    }
}
