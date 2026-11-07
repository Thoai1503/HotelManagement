using Hotel.Core.Domain.Entities;
using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Repositories.Identities
{
    public interface IUserRepository : IBaseRepository<UserModal>
    {
        Task<UserModal> GetSingleUser(string id);
        Task<bool> EmailIsExist(string email);

        Task<UserModal> GetUserByEmail(string email);
        Task<bool> Register(User user);

        Task<bool> ChangeActive(User user);

        Task<IEnumerable<User>> GetUsers(List<string> userIds);
    }
}
