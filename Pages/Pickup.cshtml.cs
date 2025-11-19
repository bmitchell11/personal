using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc; 
using System.ComponentModel.DataAnnotations;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;


namespace FinalProject.Pages
{
    public class PickupModel : PageModel
    {
        private readonly PackageService _packageService;

        public PickupModel(PackageService packageService)
        {
            _packageService = packageService;
        }

        [BindProperty]
        public string TrackingNumber { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            await _packageService.MarkAsPickedUpAsync(TrackingNumber);
            TempData["Message"] = "Package marked as picked up!";
            return RedirectToPage();
        }
    }
}
