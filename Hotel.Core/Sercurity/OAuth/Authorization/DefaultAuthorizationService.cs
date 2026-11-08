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

        public Task<UserModel> GetCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}
