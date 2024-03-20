using PMF.API.Models;

namespace PMF.API.Services
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto user);
    }
}
