using Hotel.Core.Domain.Service.Others;
using Hotel.Core.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Service.Identities;
namespace Hotel.Web.Controllers
{
    public class AccountController : BaseController<IAccountService, AccountModal>
    {
        private  IUserService _userService { get; }
        public AccountController(IAccountService service,IUserService userService) : base(service)
        {
               _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
      
            return View();
        }
    }
}
