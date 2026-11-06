using Microsoft.AspNetCore.Mvc;

namespace Hotel.Core.Mvc.Controllers
{
    public class MasterController<TService, TModel> : BaseController<TService, TModel>
    {
        public MasterController(TService service) : base(service)
        {
        }

        public virtual async Task<IActionResult> Index()
        {
            if (BaseService is null)
            {
                return NotFound();
            }

            var models = await BaseService.GetAll<TModel>();
            return View(models);
        }
        public virtual async Task<IActionResult> Details(Guid id)
        {
            if (BaseService is null)
            {
                return NotFound();
            }

            var model = await BaseService.GetById<TModel>(id);
            return model is null ? NotFound() : View(model);
        }

        [HttpGet]
        public virtual async Task<IActionResult> CreateOrUpdate(Guid? id)
        {
            if (BaseService is null)
            {
                return NotFound();
            }

            TModel model;
            if (id.HasValue)
            {
                var existingModel = await BaseService.GetById<TModel>(id.Value);
                if (existingModel is null)
                {
                    return NotFound();
                }

                model = existingModel;
            }
            else
            {
                model = Activator.CreateInstance<TModel>();
            }

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateOrUpdate(TModel model)
        {
            if (BaseService is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var modelType = typeof(TModel);
            var idProperty = modelType.GetProperty("id");
            if (idProperty is null)
            {
                throw new InvalidOperationException($"The model type '{modelType.Name}' does not have an 'id' property.");
            }

            var idValue = idProperty.GetValue(model);
            if (idValue is null || (Guid)idValue == Guid.Empty)
            {
                await BaseService.Create(model);
            }
            else
            {
                await BaseService.Update(model);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}