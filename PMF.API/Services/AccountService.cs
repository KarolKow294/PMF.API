using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMF.API.Entities;
using PMF.API.Models;

namespace PMF.API.Services
{
    public class AccountService(PmfDbContext context, IPasswordHasher<User> passwordHasher) : IAccountService
    {
        public async Task RegisterUser(RegisterUserDto user)
        {
            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId
            };
            var hashedPassword = passwordHasher.HashPassword(newUser, user.Password);

            newUser.PasswordHash = hashedPassword;
            context.User.Add(newUser);
            await context.SaveChangesAsync();
        }
    }
}
