using FinalProject.Models;

namespace FinalProject.Data.Repositories
{
    public class StaffRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Staff? ValidateLogin(string username, string password)
        {
            return _context.StaffLogin
                .FirstOrDefault(s =>
                    s.staff_username == username &&
                    s.staff_password == password
                );
        }
    }
}
