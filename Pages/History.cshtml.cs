using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public HistoryModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public int ResidentID { get; set; }

        [BindProperty]
        public int? UnitNumber { get; set; }

        public List<Package> Packages { get; set; } = new List<Package>();

        public List<Resident> Residents { get; set; } = new List<Resident>();

        public void OnGet()
        {
            Residents = _db.Residents.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Residents = _db.Residents.ToList();

            if (ResidentID == 0 || UnitNumber == null)
            {
                ModelState.AddModelError(string.Empty, "Please select a resident and enter a unit number.");
                return Page();
            }

            var resident = await _db.Residents.FirstOrDefaultAsync(r => r.ResidentID == ResidentID && r.UnitNumber == UnitNumber);

            if (resident == null)
            {
                ModelState.AddModelError(string.Empty, "Resident not found.");
                return Page();
            }

            Packages = await _db.Packages
                .Where(p => p.ResidentID == resident.ResidentID)
                .OrderByDescending(p => p.ReceivedDate)
                .ToListAsync();

            return Page();
        }
    }
}
