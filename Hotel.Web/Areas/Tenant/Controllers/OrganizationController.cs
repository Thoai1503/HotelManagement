using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Service.Tenant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Areas.Tenant.Controllers
{
    [Area("Tenant")]
    public class OrganizationController : Controller
    {
        private const long MaximumLogoSizeBytes = 2 * 1024 * 1024;
        private const int MaximumRequestSizeBytes = 3 * 1024 * 1024;
        private const string LogoUrlPrefix = "/uploads/organizations/";

        private readonly OrganizationService _organizationService;
        private readonly IWebHostEnvironment _environment;

        public OrganizationController(
            OrganizationService organizationService,
            IWebHostEnvironment environment)
        {
            _organizationService = organizationService;
            _environment = environment;
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
        [RequestSizeLimit(MaximumRequestSizeBytes)]
        public async Task<IActionResult> Create(
            OrganizationModal organization,
            IFormFile? logoFile)
        {
            Console.WriteLine($"Received organization: {organization.name}, logoFile: {logoFile?.FileName}");
            var logoValidationError = await GetLogoValidationErrorAsync(logoFile);
            if (logoValidationError is not null)
            {
                ModelState.AddModelError(nameof(logoFile), logoValidationError);
            }

            // if (!ModelState.IsValid)
            // { 
            //     Console.WriteLine("Model state is invalid. Returning to view with errors.");
            //     return View(organization);
            // }

            if (logoFile is not null)
            {
                organization.logo = await SaveLogoAsync(logoFile);
            }

            try
            {
                await _organizationService.Create(organization);
            }
            catch
            {
                DeleteLogo(organization.logo);
                throw;
            }

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
        [RequestSizeLimit(MaximumRequestSizeBytes)]
        public async Task<IActionResult> Edit(
            Guid id,
            OrganizationModal organization,
            IFormFile? logoFile)
        {
            if (id != organization.id)
            {
                return BadRequest();
            }

            var existingOrganization = await _organizationService.GetById(id);
            if (existingOrganization is null)
            {
                return NotFound();
            }

            var logoValidationError = await GetLogoValidationErrorAsync(logoFile);
            if (logoValidationError is not null)
            {
                ModelState.AddModelError(nameof(logoFile), logoValidationError);
            }

            // if (!ModelState.IsValid)
            // {
            //     organization.logo = existingOrganization.logo;
            //     return View(organization);
            // }

            if (logoFile is not null)
            {
                organization.logo = await SaveLogoAsync(logoFile);
            }
            else
            {
                organization.logo = existingOrganization.logo;
            }

            try
            {
                await _organizationService.Update(organization);
            }
            catch
            {
                if (logoFile is not null)
                {
                    DeleteLogo(organization.logo);
                }

                throw;
            }

            if (logoFile is not null)
            {
                DeleteLogo(existingOrganization.logo);
            }

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
