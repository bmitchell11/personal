using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc; 
using System.ComponentModel.DataAnnotations;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;

namespace FinalProject.Pages
{
    public class UnknownPackageModel : PageModel
    {
        private readonly PackageService _packageService;

        public UnknownPackageModel(PackageService packageService)
        {
            _packageService = packageService;
        }

        [BindProperty]
        public string NameOnPackage { get; set; } = string.Empty;

        [BindProperty]
        public string PostalService { get; set; } = string.Empty;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            _packageService.AddUnknownPackage(NameOnPackage, PostalService);
            TempData["Message"] = "Unknown package logged!";
            return RedirectToPage();
        }
    }
}
