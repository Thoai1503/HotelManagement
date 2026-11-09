using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Service.Others;

namespace Hotel.Core.Sercurity.OAuth.Authorization
{
    public class DefaultAuthorizationService : IDefaultAuthorizationService
    {

        private IHttpContextAccessor _context;
        private ISession _session;
        private IAccountService _accountService;
        private HttpRequest _request;

        public DefaultAuthorizationService(IHttpContextAccessor context, IAccountService accountService)
        {
            _context = context;
            _session = _context.HttpContext.Session;
            _accountService = accountService;
            _request = _context.HttpContext.Request;
        }

        public async Task<UserModel> GetCurrentUser()
        {
            var userBytes = _session.Get(StorageConst.CURRENT_USER);
            if ((userBytes == null) && _context.HttpContext.User.Identity.IsAuthenticated)
            {
                var email = _context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                if (!string.IsNullOrEmpty(email))
                {
                    var userModel = await _accountService.GetUserByEmailAsync(email);
                    if (userModel != null)
                    {
                        var serializedUser = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(userModel);
                        _session.Set(StorageConst.CURRENT_USER, serializedUser);
                        return userModel;
                    }
                }
            }
            if (userBytes != null)
            {

                var userModel = DeserializeUserModel(userBytes);
                return userModel;
            }
            return null;
        }

        // Add this private helper method for deserialization if not already present
        private UserModel DeserializeUserModel(byte[] data)
        {
            // Implement your deserialization logic here, e.g. using JSON or BinaryFormatter
            // Example using System.Text.Json:
            return System.Text.Json.JsonSerializer.Deserialize<UserModel>(data);
        }
    }
}
