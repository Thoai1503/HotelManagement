using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Service.Tenant;
using Hotel.Core.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Areas.Tenant.Controllers
{
    [Area("Tenant")]
    // public class HotelController : BaseController<IHotelService, HotelModal>
    // {
    //     private readonly IOrganizationService _organizationService;

    //     public HotelController(
    //         IHotelService hotelService,
    //         IOrganizationService organizationService)
    //         : base(hotelService)
    //     {
    //         _organizationService = organizationService;
    //     }

    //     public async Task<IActionResult> Index()
    //     {
    //         var hotels = await Service.GetAll();
    //         var organizations = await _organizationService.GetAll();
    //         var organizationNames = organizations.ToDictionary(
    //             organization => organization.id,
    //             organization => organization.name);

    //         foreach (var hotel in hotels)
    //         {
    //             hotel.organizationName = organizationNames.TryGetValue(
    //                 hotel.organizationId,
    //                 out var organizationName)
    //                 ? organizationName
    //                 : "Unknown organization";
    //         }

    //         return View(hotels);
    //     }

    //     public async Task<IActionResult> Details(Guid id)
    //     {
    //         var hotel = await Service.GetById(id);
    //         if (hotel is null)
    //         {
    //             return NotFound();
    //         }

    //         var organization = await _organizationService.GetById(hotel.organizationId);
    //         hotel.organizationName = organization?.name ?? "Unknown organization";
    //         return View(hotel);
    //     }

    //     [HttpGet]
    //     public async Task<IActionResult> CreateOrUpdate(Guid? id)
    //     {
    //         HotelModal hotel;
    //         if (id.HasValue)
    //         {
    //             var existingHotel = await Service.GetById(id.Value);
    //             if (existingHotel is null)
    //             {
    //                 return NotFound();
    //             }

    //             hotel = existingHotel;
    //             hotel.isEditMode = true;
    //         }
    //         else
    //         {
    //             hotel = new HotelModal();
    //         }

    //         await SetOrganizationsAsync();
    //         return View(hotel);
    //     }

    //     [HttpPost]
    //     [ValidateAntiForgeryToken]
    //     public async Task<IActionResult> CreateOrUpdate(
    //         [FromRoute] Guid? id,
    //         HotelModal hotel)
    //     {
    //         if (id.HasValue && id.Value != hotel.id)
    //         {
    //             return BadRequest();
    //         }

    //         HotelModal? existingHotel = null;
    //         if (id.HasValue)
    //         {
    //             existingHotel = await Service.GetById(id.Value);
    //             if (existingHotel is null)
    //             {
    //                 return NotFound();
    //             }
    //         }

    //         if (!await OrganizationExistsAsync(hotel.organizationId))
    //         {
    //             ModelState.AddModelError(
    //                 nameof(hotel.organizationId),
    //                 "Select a valid organization.");
    //         }

    //         if (!ModelState.IsValid)
    //         {
    //             hotel.isEditMode = id.HasValue;
    //             await SetOrganizationsAsync();
    //             return View(hotel);
    //         }

    //         if (existingHotel is null)
    //         {
    //             await Service.Create(hotel);
    //         }
    //         else
    //         {
    //             await Service.Update(hotel);
    //         }

    //         return RedirectToAction(nameof(Index));
    //     }

    //     [HttpGet]
    //     public IActionResult Create() => RedirectToAction(nameof(CreateOrUpdate));

    //     [HttpGet]
    //     public IActionResult Edit(Guid id) =>
    //         RedirectToAction(nameof(CreateOrUpdate), new { id });

    //     [HttpGet]
    //     public async Task<IActionResult> Delete(Guid id)
    //     {
    //         var hotel = await Service.GetById(id);
    //         if (hotel is null)
    //         {
    //             return NotFound();
    //         }

    //         var organization = await _organizationService.GetById(hotel.organizationId);
    //         hotel.organizationName = organization?.name ?? "Unknown organization";
    //         return View(hotel);
    //     }

    //     [HttpPost, ActionName("Delete")]
    //     [ValidateAntiForgeryToken]
    //     public async Task<IActionResult> DeleteConfirmed(Guid id)
    //     {
    //         var hotel = await Service.GetById(id);
    //         if (hotel is null)
    //         {
    //             return NotFound();
    //         }

    //         await Service.Delete(hotel);
    //         return RedirectToAction(nameof(Index));
    //     }

    //     private async Task<bool> OrganizationExistsAsync(Guid organizationId)
    //     {
    //         var organizations = await _organizationService.GetAll();
    //         return organizations.Any(organization => organization.id == organizationId);
    //     }

    //     private async Task SetOrganizationsAsync()
    //     {
    //         ViewData["Organizations"] = await _organizationService.GetAll();
    //     }
    // }
    public class HotelController : MasterController<IHotelService, HotelModal>
    {
        private readonly IOrganizationService _organizationService;

        public HotelController(
            IHotelService hotelService,
            IOrganizationService organizationService)
            : base(hotelService)
        {
            _organizationService = organizationService;
        }

        protected  async Task SetViewDataAsync()
        {
            ViewData["Organizations"] = await _organizationService.GetAll();
        }
      public override async Task<IActionResult> Index()
        {
            var hotels = await Service.GetAll();
            var organizations = await _organizationService.GetAll();
            var organizationNames = organizations.ToDictionary(
                organization => organization.id,
                organization => organization.name);

            foreach (var hotel in hotels)
            {
                hotel.organizationName = organizationNames.TryGetValue(
                    hotel.organizationId,
                    out var organizationName)
                    ? organizationName
                    : "Unknown organization";
            }

            return View(hotels);
        }
        public override async Task<IActionResult> CreateOrUpdate(Guid? id)
        {
            await SetViewDataAsync();
            return await base.CreateOrUpdate(id);
        }
    }
}