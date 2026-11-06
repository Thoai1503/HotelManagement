using Hotel.Core.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Core.Domain.Mvc.Controllers
{
    public class BaseController : Controller
    {
        public string ClassName { get; set; }
        public ISession Session
        {
            get
            {
                return HttpContext.Session;
            }
        }
        public BaseController()
        {
            // Init class name
            ClassName = GetType().Name;
        } 
        public override ViewResult View(object? model)
        {
            ViewBag.ClassName = ClassName;
            return base.View(model);
        }
    }
    public class BaseController<TModel> : BaseController 
    {
        public TModel Model { get; private set; } = Activator.CreateInstance<TModel>();
        public BaseController() : base()
        {
            Model = Activator.CreateInstance<TModel>();
        }
        public override ViewResult View()
        {
            return View(Model);
        }
    }

    public class BaseController<TService, TModel> : BaseController<TModel>
    {
        public TService Service;

        public IBaseService? BaseService;
        public BaseController(TService service) : base()
        {
            Service = service;
            BaseService = service as IBaseService;
        }
    }

}
