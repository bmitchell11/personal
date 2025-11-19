using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data.Repositories
{
    public class PackageRepository
    {
        private readonly ApplicationDbContext _context;

        public PackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Package pkg)
        {
            _context.Packages.Add(pkg);
            _context.SaveChanges();
        }

        public Package? GetByTracking(string tracking)
        {
            return _context.Packages
                .Include(p => p.Resident)
                .FirstOrDefault(p => p.TrackingNumber == tracking);
        }

        public void Update(Package pkg)
        {
            _context.Packages.Update(pkg);
            _context.SaveChanges();
        }

        public List<Package> GetHistory(string? residentName, int? unitNumber)
        {
            var query = _context.Packages
                .Include(p => p.Resident)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(residentName))
                query = query.Where(p => p.Resident!.FullName.Contains(residentName));

            if (unitNumber != null)
                query = query.Where(p => p.Resident!.UnitNumber == unitNumber);

            return query
                .OrderByDescending(p => p.ReceivedDate)
                .ToList();
        }
    }
}
