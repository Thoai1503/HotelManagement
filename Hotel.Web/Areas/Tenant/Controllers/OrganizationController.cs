using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Service.Tenant;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Areas.Tenant.Controllers
{
    [Area("Tenant")]
    public class OrganizationController : Controller
    {
        private readonly OrganizationService _organizationService;

        public OrganizationController(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task<IActionResult> Index()
        {
            var organizations = await _organizationService.GetAll();
            return View(organizations);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var organization = await _organizationService.GetById(id);
            return organization is null ? NotFound() : View(organization);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new OrganizationModal());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrganizationModal organization)
        {
            if (!ModelState.IsValid)
            {
                return View(organization);
            }

            await _organizationService.Create(organization);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var organization = await _organizationService.GetById(id);
            return organization is null ? NotFound() : View(organization);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrganizationModal organization)
        {
            if (id != organization.id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(organization);
            }

            var existingOrganization = await _organizationService.GetById(id);
            if (existingOrganization is null)
            {
                return NotFound();
            }

            await _organizationService.Update(organization);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var organization = await _organizationService.GetById(id);
            return organization is null ? NotFound() : View(organization);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var organization = await _organizationService.GetById(id);
            if (organization is null)
            {
                return NotFound();
            }

            await _organizationService.Delete(organization);
            return RedirectToAction(nameof(Index));
        }
    }
}
