using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc; 
using System.ComponentModel.DataAnnotations;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;


namespace FinalProject.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var staff = _authService.Login(Username, Password);

            if (staff == null)
            {
                ErrorMessage = "Invalid username or password";
                return Page();
            }

            HttpContext.Session.SetString("StaffUser", staff.staff_username);
            return RedirectToPage("/Dashboard");
        }
    }
}
