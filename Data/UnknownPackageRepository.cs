using FinalProject.Models;

namespace FinalProject.Data.Repositories
{
    public class UnknownPackageRepository
    {
        private readonly ApplicationDbContext _context;

        public UnknownPackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(UnknownPackage pkg)
        {
            _context.UnknownPackages.Add(pkg);
            _context.SaveChanges();
        }

        public List<UnknownPackage> GetAll()
        {
            return _context.UnknownPackages
                .OrderByDescending(p => p.DeliveredDate)
                .ToList();
        }
    }
}
