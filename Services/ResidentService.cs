using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class ResidentService
    {
        private readonly ApplicationDbContext _db;

        public ResidentService(ApplicationDbContext db)
        {
            _db = db;
        }

        // Get all residents for dropdowns
        public List<Resident> GetAllResidents()
        {
            return _db.Residents.ToList();
        }

        // Search by name and/or unit number
        public List<Resident> SearchResidents(string? name, int? unitNumber)
        {
            var query = _db.Residents.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(r => r.FullName.Contains(name));

            if (unitNumber.HasValue)
                query = query.Where(r => r.UnitNumber == unitNumber.Value);

            return query.ToList();
        }
    }
}
