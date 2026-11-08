using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Repositories.Identities;
using Hotel.Core.Domain.Entities;

namespace Hotel.Core.Domain.Service.Identities
{
    public class UserService : BaseService<IUserRepository>, IUserService
    {
        public UserService(IUserRepository repository, IHttpContextAccessor httpContextAccessor)
            : base(repository, httpContextAccessor)
        {
        }

        public Task<bool> EmailIsExist(string email) =>
            Repository.EmailIsExist(email);

        public Task<UserModel?> GetUserByEmail(string email) =>
            Repository.GetUserByEmail(email);

        public Task<bool> Register(UserModel user)
        {
            var entity = new User
            {
                Id = user.id,
                Username = user.userName,
                PasswordHash = user.passwordHash,
                Email = user.email,
                PhoneNumber = user.phoneNumber,
                AvatarUrl = user.avatarUrl,
                IsActive = user.isActive,
                CreatedAtUtc = user.createdAtUtc
            };

            return Repository.Register(entity);
        }
    }
}
