using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalProject.Models;
using FinalProject.Services;

namespace FinalProject.Pages
{
    public class ReceiveModel : PageModel
    {
        private readonly PackageService _packageService;
        private readonly ResidentService _residentService;

        public ReceiveModel(PackageService packageService, ResidentService residentService)
        {
            _packageService = packageService;
            _residentService = residentService;
        }

        [BindProperty]
        public int ResidentID { get; set; }

        [BindProperty]
        public string PostalService { get; set; } = string.Empty;

        public List<Resident> Residents { get; set; } = new List<Resident>();
        public List<SelectListItem> PostalServiceOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("USPS", "USPS"),
            new SelectListItem("FedEx", "FedEx"),
            new SelectListItem("UPS", "UPS")
        };


        public void OnGet()
        {
            // Retrieve residents from database
            Residents = _residentService.GetAllResidents();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ResidentID == 0 || string.IsNullOrEmpty(PostalService))
            {
                TempData["Message"] = "Please select a resident and enter postal service.";
                return RedirectToPage();
            }

            // Add package to database
            await _packageService.AddPackageAsync(ResidentID, PostalService);

            TempData["Message"] = "Package logged and email sent!";
            return RedirectToPage();
        }
    }
}
