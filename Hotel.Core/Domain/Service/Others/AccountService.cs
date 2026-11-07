using Hotel.Core.Domain.Entities;
using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Repositories.Identities;
using Hotel.Core.Domain.Repositories.Others;
using Hotel.Core.Sercurity.Cryptography;
using System.Security.Claims;
namespace Hotel.Core.Domain.Service.Others
{
    public class AccountService : BaseService<IAccountRepository>, IAccountService
    {

        private IUserRepository _userRepository;
        public AccountService(IAccountRepository repository, IHttpContextAccessor httpContextAccessor,IUserRepository userRepository) : base(repository, httpContextAccessor)
        {
            _userRepository = userRepository;
        }



        public bool IsPasswordMatched(string inputPassword, string password, string salt)
        {
            var hasher = new PasswordHasher();
            var hashedPassword = hasher.HashPassword(inputPassword, salt);
            return hashedPassword == password;
        }
        public ClaimsIdentity CreateIdentity(string auhthenticationType, UserModal user)
        {
            var identity = new ClaimsIdentity(auhthenticationType);
            identity.AddClaim(new Claim("user_id", user.id.ToString()));
            identity.AddClaim(new Claim("user_name", user.userName));
            identity.AddClaim(new Claim("issue_date",
                DateTimeOffset.Now.ToUniversalTime().UtcTicks.ToString()));
            return identity;
        }
    }
}
