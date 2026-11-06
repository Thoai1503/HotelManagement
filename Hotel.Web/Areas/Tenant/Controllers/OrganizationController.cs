using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Service.Tenant;
using Hotel.Core.Mvc.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Areas.Tenant.Controllers
{
    [Area("Tenant")]
    public class OrganizationController : BaseController<IOrganizationService,OrganizationModal>
    {
        private const long MaximumLogoSizeBytes = 2 * 1024 * 1024;
        private const int MaximumRequestSizeBytes = 3 * 1024 * 1024;
        private const string LogoUrlPrefix = "/uploads/organizations/";

        private readonly IWebHostEnvironment _environment;

        public OrganizationController(
            IOrganizationService service,
            IWebHostEnvironment environment)
            : base(service)
        {
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var organizations = await Service.GetAll();
            return View(organizations);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var organization = await Service.GetById(id);
            return organization is null ? NotFound() : View(organization);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrUpdate(Guid? id)
        {
            OrganizationModal organization;
            if (id.HasValue)
            {
                var existingOrganization = await Service.GetById(id.Value);
                if (existingOrganization is null)
                {
                    return NotFound();
                }

                organization = existingOrganization;
                organization.isEditMode = true;
            }
            else
            {
                organization = new OrganizationModal();
            }

            return View(organization);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(MaximumRequestSizeBytes)]
        public async Task<IActionResult> CreateOrUpdate(
            [FromRoute] Guid? id,
            OrganizationModal organization,
            IFormFile? logoFile)
        {
            if (id.HasValue && id.Value != organization.id)
            {
                return BadRequest();
            }

            OrganizationModal? existingOrganization = null;
            if (id.HasValue)
            {
                existingOrganization = await Service.GetById(id.Value);
                if (existingOrganization is null)
                {
                    return NotFound();
                }
            }

            var logoValidationError = await GetLogoValidationErrorAsync(logoFile);
            if (logoValidationError is not null)
            {
                ModelState.AddModelError(nameof(logoFile), logoValidationError);
            }

            if (!ModelState.IsValid)
            {
                organization.isEditMode = id.HasValue;
                if (existingOrganization is not null)
                {
                    organization.logo = existingOrganization.logo;
                }

                return View(organization);
            }

            if (logoFile is not null)
            {
                organization.logo = await SaveLogoAsync(logoFile);
            }
            else if (existingOrganization is not null)
            {
                organization.logo = existingOrganization.logo;
            }

            try
            {
                if (existingOrganization is null)
                {
                    await Service.Create(organization);
                }
                else
                {
                    await Service.Update(organization);
                }
            }
            catch
            {
                if (logoFile is not null)
                {
                    DeleteLogo(organization.logo);
                }

                throw;
            }

            if (logoFile is not null && existingOrganization is not null)
            {
                DeleteLogo(existingOrganization.logo);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() => RedirectToAction(nameof(CreateOrUpdate));

        [HttpGet]
        public IActionResult Edit(Guid id) =>
            RedirectToAction(nameof(CreateOrUpdate), new { id });

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var organization = await Service.GetById(id);
            return organization is null ? NotFound() : View(organization);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var organization = await Service.GetById(id);
            if (organization is null)
            {
                return NotFound();
            }

            await Service.Delete(organization);
            DeleteLogo(organization.logo);
            return RedirectToAction(nameof(Index));
        }

        private static async Task<string?> GetLogoValidationErrorAsync(IFormFile? file)
        {
            if (file is null)
            {
                return null;
            }

            if (file.Length == 0)
            {
                return "The selected logo file is empty.";
            }

            if (file.Length > MaximumLogoSizeBytes)
            {
                return "The logo file must be 2 MB or smaller.";
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension is not (".png" or ".jpg" or ".jpeg" or ".webp"))
            {
                return "Upload a PNG, JPG, or WebP image.";
            }

            var signature = new byte[12];
            await using var stream = file.OpenReadStream();
            var bytesRead = await stream.ReadAsync(signature);

            var isPng = extension == ".png" &&
                bytesRead >= 8 &&
                signature.AsSpan(0, 8).SequenceEqual(
                    new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 });
            var isJpeg = extension is ".jpg" or ".jpeg" &&
                bytesRead >= 3 &&
                signature[0] == 0xFF &&
                signature[1] == 0xD8 &&
                signature[2] == 0xFF;
            var isWebp = extension == ".webp" &&
                bytesRead >= 12 &&
                signature.AsSpan(0, 4).SequenceEqual("RIFF"u8) &&
                signature.AsSpan(8, 4).SequenceEqual("WEBP"u8);

            return isPng || isJpeg || isWebp
                ? null
                : "The file contents do not match a supported image format.";
        }

        private async Task<string> SaveLogoAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid():N}{extension}";
            var uploadDirectory = GetLogoDirectory();
            Directory.CreateDirectory(uploadDirectory);

            var filePath = Path.Combine(uploadDirectory, fileName);
            await using var stream = new FileStream(
                filePath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            await file.CopyToAsync(stream);

            return LogoUrlPrefix + fileName;
        }

        private string GetLogoDirectory()
        {
            var webRootPath = _environment.WebRootPath;
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
            }

            return Path.Combine(webRootPath, "uploads", "organizations");
        }

        private void DeleteLogo(string? logoUrl)
        {
            if (logoUrl is null || !logoUrl.StartsWith(LogoUrlPrefix, StringComparison.Ordinal))
            {
                return;
            }

            var fileName = logoUrl[LogoUrlPrefix.Length..];
            if (Path.GetFileName(fileName) != fileName)
            {
                return;
            }

            var filePath = Path.Combine(GetLogoDirectory(), fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
