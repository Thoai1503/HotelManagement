using Hotel.Core.Domain.Infrastructure.Models;
using System.Security.Claims;

namespace Hotel.Core.Domain.Service.Others
{
    public interface IAccountService
    {
        Task<UserModel?> AuthenticateAsync(string email, string password);
        Task<UserModel?> RegisterAsync(
            string userName,
            string email,
            string phoneNumber,
            string password);
        bool IsPasswordMatched(string inputPassword, string passwordHash);
        ClaimsIdentity CreateIdentity(string authenticationType, UserModel user);
        Task<UserModel?> GetUserByEmailAsync(string email);
    }
}
