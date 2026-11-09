using Hotel.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Core.Domain.Repositories.Identities
{
    public class AdminUserRepository : BaseRepository<AdminUser, AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AdminUser?> GetByUsername(string username)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(username);

            return DbContext.AdminUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(adminUser => adminUser.Username == username);
        }

        public Task<AdminUser?> GetByEmail(string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            return DbContext.AdminUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(adminUser => adminUser.Email == email);
        }
    }
}
