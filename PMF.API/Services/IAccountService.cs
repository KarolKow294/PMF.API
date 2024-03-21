using PMF.API.Models;

namespace PMF.API.Services
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto user);
        Task<string> GenerateJwtAsync(LoginDto userDto);
    }
}
