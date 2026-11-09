using Hotel.Core.Domain.Entities;
using Hotel.Core.Domain.Repositories;

namespace Hotel.Core.Domain.Repositories.Identities
{
    public interface IAdminUserRepository : IBaseRepository<AdminUser>
    {
        Task<AdminUser?> GetByUsername(string username);
        Task<AdminUser?> GetByEmail(string email);
    }
}
