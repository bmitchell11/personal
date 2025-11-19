using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc; 
using System.ComponentModel.DataAnnotations;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;


namespace FinalProject.Pages
{
    public class DeliverModel : PageModel
    {
        private readonly PackageService _packageService;
        private readonly ResidentService _residentService;

        public DeliverModel(PackageService packageService, ResidentService residentService)
        {
            _packageService = packageService;
            _residentService = residentService;
        }

        [BindProperty]
        public int ResidentID { get; set; }

        [BindProperty]
        public string TrackingNumber { get; set; } = string.Empty;

        public List<Resident> Residents { get; set; }

        public void OnGet()
        {
            Residents = _residentService.GetAllResidents();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _packageService.AddPackageAsync(ResidentID, TrackingNumber);
            TempData["Message"] = "Package logged and email sent!";
            return RedirectToPage();
        }
    }
}
