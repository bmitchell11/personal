using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data.Repositories
{
    public class ResidentRepository
    {
        private readonly ApplicationDbContext _context;

        public ResidentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Resident> GetAll()
        {
            return _context.Residents.OrderBy(r => r.FullName).ToList();
        }

        public Resident? GetById(int id)
        {
            return _context.Residents.Find(id);
        }

        public List<Resident> Search(string? name, int? unitNumber)
        {
            var query = _context.Residents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(r => r.FullName.Contains(name));

            if (unitNumber != null)
                query = query.Where(r => r.UnitNumber == unitNumber);

            return query.ToList();
        }
    }
}
