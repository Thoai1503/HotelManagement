using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Repositories.Identities;
using Hotel.Core.Domain.Repositories.Others;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Hotel.Core.Domain.Service.Others
{
    public class AccountService : BaseService<IAccountRepository>, IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<UserModel> _passwordHasher = new();

        public AccountService(IUserRepository userRepository,IAccountRepository accountRepository,IHttpContextAccessor httpContext) : base(accountRepository,httpContext)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(NormalizeEmail(email));
            if (user is null || !user.isActive ||
                !IsPasswordMatched(password, user.passwordHash))
            {
                return null;
            }

            return user;
        }

        public async Task<UserModel?> RegisterAsync(
            string userName,
            string email,
            string phoneNumber,
            string password)
        {
            email = NormalizeEmail(email);
            if (await _userRepository.EmailIsExist(email))
            {
                return null;
            }

            var user = new UserModel
            {
                id = Guid.NewGuid(),
                userName = userName.Trim(),
                email = email,
                phoneNumber = phoneNumber.Trim(),
                passwordHash = _passwordHasher.HashPassword(new UserModel(), password),
                avatarUrl = string.Empty,
                isActive = true,
                createdAtUtc = DateTime.UtcNow
            };

            return await _userRepository.Insert(user);
        }

        public bool IsPasswordMatched(string inputPassword, string passwordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                new UserModel(),
                passwordHash,
                inputPassword);
            return result is PasswordVerificationResult.Success
                or PasswordVerificationResult.SuccessRehashNeeded;
        }

        public ClaimsIdentity CreateIdentity(string authenticationType, UserModel user)
        {
            var identity = new ClaimsIdentity(authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.userName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.email));
            identity.AddClaim(new Claim("user_id", user.id.ToString()));
            identity.AddClaim(new Claim("user_name", user.userName));
            return identity;
        }

        private static string NormalizeEmail(string email) =>
            email.Trim().ToLowerInvariant();

        public Task<UserModel?> GetUserByEmailAsync(string email)
        {
           
            return _userRepository.GetUserByEmail(NormalizeEmail(email));
        }
    }
}
