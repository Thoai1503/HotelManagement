using Hotel.Core.Domain.Entities;
using Hotel.Core.Domain.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Core.Domain.Repositories.Identities
{
    public class UserRepository : BaseRepository<UserModel, User>, IUserRepository
    {
        public UserRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ChangeActive(User user)
        {
            var existingUser = await DbContext.Users.FindAsync(user.Id);
            if (existingUser is null)
            {
                return false;
            }

            existingUser.IsActive = user.IsActive;
            await DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailIsExist(string email)
        {
            return await DbContext.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<UserModel?> GetSingleUser(Guid id)
        {
            return await GetById(id);
        }

        public async Task<UserModel?> GetUserByEmail(string email)
        {
            var entity = await DbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == email);

            if (entity is null)
            {
                return null;
            }

            return new UserModel
            {
                id = entity.Id,
                userName = entity.Username,
                passwordHash = entity.PasswordHash,
                email = entity.Email,
                phoneNumber = entity.PhoneNumber,
                avatarUrl = entity.AvatarUrl,
                isActive = entity.IsActive,
                createdAtUtc = entity.CreatedAtUtc
            };
        }

        public async Task<IEnumerable<User>> GetUsers(List<string> userIds)
        {
            var ids = userIds.Select(Guid.Parse).ToList();
            return await DbContext.Users
                .AsNoTracking()
                .Where(user => ids.Contains(user.Id))
                .ToListAsync();
        }

        public async Task<bool> Register(User user)
        {
            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();
            return true;
        }
    }
}
