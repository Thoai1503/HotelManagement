using Hotel.Core.Domain.Entities;
using Hotel.Core.Domain.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Core.Domain.Repositories.Identities
{
    public class UserRepository : BaseRepository<User, UserModal>, IUserRepository
    {
        public UserRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> ChangeActive(User user)
        {
            throw new NotImplementedException();
        }

        public Task<UserModal> Delete(UserModal entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailIsExist(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetSingleUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserModal> GetUserByEmail(string email)
        {
        
            var model = new UserModal();
            var entity = DbContext.Users.FirstOrDefault(u => u.Email == email);
            if (entity == null) {
               
                return Task.FromResult<UserModal>(null);
            }
            else
            {
                model.id = entity.Id;
                model.userName = entity.Username;
                model.passwordHash = entity.PasswordHash;
                model.email = entity.Email;
                model.phoneNumber = entity.PhoneNumber;
                model.avatarUrl = entity.AvatarUrl;
                model.isActive = entity.IsActive;
                model.createdAtUtc = entity.CreatedAtUtc;
                return Task.FromResult(model);
            }
 
          
        }

        public Task<IEnumerable<User>> GetUsers(List<string> userIds)
        {
            throw new NotImplementedException();
        }

        public Task<UserModal> Insert(UserModal entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<UserModal> Update(UserModal entity)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<UserModal>> IBaseRepository<UserModal>.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<UserModal?> IBaseRepository<UserModal>.GetById(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        Task<UserModal> IUserRepository.GetSingleUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
