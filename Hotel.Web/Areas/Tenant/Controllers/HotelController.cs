using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Service.Tenant;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Areas.Tenant.Controllers
{
    [Area("Tenant")]
    public class HotelController : Controller
    {
        private readonly HotelService _hotelService;
        private readonly OrganizationService _organizationService;

        public HotelController(
            HotelService hotelService,
            OrganizationService organizationService)
        {
            _hotelService = hotelService;
            _organizationService = organizationService;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelService.GetAll();
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

        public async Task<IActionResult> Details(Guid id)
        {
            var hotel = await _hotelService.GetById(id);
            if (hotel is null)
            {
                return NotFound();
            }

            var organization = await _organizationService.GetById(hotel.organizationId);
            hotel.organizationName = organization?.name ?? "Unknown organization";
            return View(hotel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await SetOrganizationsAsync();
            return View(new HotelModal());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HotelModal hotel)
        {
            if (!await OrganizationExistsAsync(hotel.organizationId))
            {
                ModelState.AddModelError(
                    nameof(hotel.organizationId),
                    "Select a valid organization.");
            }

            if (!ModelState.IsValid)
            {
                await SetOrganizationsAsync();
                return View(hotel);
            }

            await _hotelService.Create(hotel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var hotel = await _hotelService.GetById(id);
            if (hotel is null)
            {
                return NotFound();
            }

            await SetOrganizationsAsync();
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, HotelModal hotel)
        {
            if (id != hotel.id)
            {
                return BadRequest();
            }

            if (await _hotelService.GetById(id) is null)
            {
                return NotFound();
            }

            if (!await OrganizationExistsAsync(hotel.organizationId))
            {
                ModelState.AddModelError(
                    nameof(hotel.organizationId),
                    "Select a valid organization.");
            }

            if (!ModelState.IsValid)
            {
                await SetOrganizationsAsync();
                return View(hotel);
            }

            await _hotelService.Update(hotel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var hotel = await _hotelService.GetById(id);
            if (hotel is null)
            {
                return NotFound();
            }

            var organization = await _organizationService.GetById(hotel.organizationId);
            hotel.organizationName = organization?.name ?? "Unknown organization";
            return View(hotel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hotel = await _hotelService.GetById(id);
            if (hotel is null)
            {
                return NotFound();
            }

            await _hotelService.Delete(hotel);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrganizationExistsAsync(Guid organizationId)
        {
            var organizations = await _organizationService.GetAll();
            return organizations.Any(organization => organization.id == organizationId);
        }

        private async Task SetOrganizationsAsync()
        {
            ViewData["Organizations"] = await _organizationService.GetAll();
        }
    }
}