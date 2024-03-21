using PMF.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PMF.API.Services
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto user);
        Task<string> GenerateJwtAsync(LoginDto userDto);
        Task ChangePassword(ChangePasswordDto data);
    }
}
