using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegisterUserDto user)
        {
            await accountService.RegisterUserAsync(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task <ActionResult> LoginAsync([FromBody] LoginDto user)
        {
            string token = await accountService.GenerateJwtAsync(user);
            return Ok(token);
        }

        [HttpPost("password")]
        public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto data)
        {
            await accountService.ChangePassword(data);
            return Ok();
        }
    }
}
