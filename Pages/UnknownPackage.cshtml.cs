using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;

namespace FinalProject.Pages
{
    public class UnknownPackageModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UnknownPackageModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public string NameOnPackage { get; set; } = string.Empty;

        [BindProperty]
        public string PostalService { get; set; } = string.Empty;

        public List<UnknownPackage> UnknownPackages { get; set; } = new List<UnknownPackage>();

        public List<SelectListItem> PostalServiceOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("USPS", "USPS"),
            new SelectListItem("FedEx", "FedEx"),
            new SelectListItem("UPS", "UPS")
        };

        public void OnGet()
        {
            UnknownPackages = _db.UnknownPackages
                                .OrderByDescending(u => u.DeliveredDate)
                                .ToList();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(NameOnPackage) || string.IsNullOrEmpty(PostalService))
            {
                TempData["Message"] = "Please enter both name and postal service.";
                return RedirectToPage();
            }

            var unknownPackage = new UnknownPackage
            {
                NameOnPackage = NameOnPackage,
                PostalService = PostalService,
                DeliveredDate = DateTime.Now
            };

            _db.UnknownPackages.Add(unknownPackage);
            _db.SaveChanges();

            TempData["Message"] = "Unknown package logged successfully!";
            return RedirectToPage();
        }
    }
}
