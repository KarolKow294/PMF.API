using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PMF.API.Entities;
using PMF.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMF.API.Services
{
    public class AccountService(PmfDbContext dbContext, IPasswordHasher<User> passwordHasher,
        AutenticationSettings authenticationSettings) : IAccountService
    {
        public async Task RegisterUserAsync(RegisterUserDto user)
        {
            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            var hashedPassword = passwordHasher.HashPassword(newUser, user.Password);

            newUser.PasswordHash = hashedPassword;
            dbContext.User.Add(newUser);
            await dbContext.SaveChangesAsync();
        }

        public async Task<string> GenerateJwtAsync(LoginDto userDto)
        {
            var user = await dbContext.User
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == userDto.Email);

            if (user == null)
                throw new Exception("Invalid username or password");

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid username or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public async Task ChangePassword(ChangePasswordDto data)
        {
            var user = await dbContext.User.FirstOrDefaultAsync(u => u.Id == data.Id);

            var hashedPassword = passwordHasher.HashPassword(user, data.Password);
            user.PasswordHash = hashedPassword;

            await dbContext.SaveChangesAsync();
        }
    }
}
