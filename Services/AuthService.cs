using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _db;

        public AuthService(ApplicationDbContext db)
        {
            _db = db;
        }

        // Validate username/password
        public Staff? Login(string username, string password)
        {
            return _db.StaffLogin
                      .FirstOrDefault(s => s.staff_username == username && s.staff_password == password);
        }
    }
}
