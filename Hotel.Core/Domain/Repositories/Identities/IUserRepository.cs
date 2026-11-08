using Hotel.Core.Domain.Entities;
using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Repositories.Identities
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel?> GetSingleUser(Guid id);
        Task<bool> EmailIsExist(string email);

        Task<UserModel?> GetUserByEmail(string email);
        Task<bool> Register(User user);

        Task<bool> ChangeActive(User user);

        Task<IEnumerable<User>> GetUsers(List<string> userIds);
    }
}
