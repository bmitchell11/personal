using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using FinalProject.Services;

namespace FinalProject.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly PackageService _packageService;

        public DashboardModel(PackageService packageService)
        {
            _packageService = packageService;
        }

        public List<Package> PendingPackages { get; set; } = new List<Package>();

        public void OnGet()
        {
            // Load all packages that are pending (Status = false)
            PendingPackages = _packageService.GetPendingPackages();
        }

        public async Task<IActionResult> OnPostAsync(int ResidentID, string PostalService)
        {
            if (ResidentID <= 0 || string.IsNullOrEmpty(PostalService))
            {
                TempData["Message"] = "Invalid package selection!";
                return RedirectToPage();
            }

            bool success = await _packageService.MarkAsPickedUpAsync(ResidentID, PostalService);

            TempData["Message"] = success
                ? "Package marked as picked up!"
                : "Failed to mark package as picked up.";

            return RedirectToPage();
        }
    }
}
