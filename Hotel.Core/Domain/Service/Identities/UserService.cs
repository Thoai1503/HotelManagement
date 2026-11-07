using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Service.Identities
{
    public class UserService : BaseService<UserModal>, IUserService
    {
        public UserService(UserModal repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }
    }
}
