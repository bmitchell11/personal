using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc; 
using System.ComponentModel.DataAnnotations;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;

namespace FinalProject.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly PackageService _packageService;

        public HistoryModel(PackageService packageService)
        {
            _packageService = packageService;
        }

        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public int? UnitNumber { get; set; }

        public List<Package> Packages { get; set; }

        public void OnGet() { }

        public void OnPost()
        {
            Packages = _packageService.SearchHistory(Name, UnitNumber);
        }
    }
}
