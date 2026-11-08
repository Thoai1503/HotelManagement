using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Service.Identities
{
    public interface IUserService
    {
        Task<bool> EmailIsExist(string email);
        Task<UserModel?> GetUserByEmail(string email);
        Task<bool> Register(UserModel user);
    }
}
