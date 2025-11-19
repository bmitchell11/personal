using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class PackageService
    {
        private readonly ApplicationDbContext _db;
        private readonly EmailSender _email;

        public PackageService(ApplicationDbContext db, EmailSender email)
        {
            _db = db;
            _email = email;
        }

        // Deliver a package
        public async Task AddPackageAsync(int residentID, string trackingNumber)
        {
            var resident = await _db.Residents.FindAsync(residentID);

            if (resident == null) return;

            var package = new Package
            {
                ResidentID = residentID,
                TrackingNumber = trackingNumber,
                ReceivedDate = DateTime.Now,
                Status = "Delivered"
            };

            _db.Packages.Add(package);
            await _db.SaveChangesAsync();

            _email.SendEmail(
                senderEmail: "noreply@buffteks.org",
                password: "cidm4360fall2024@*",
                toEmail: resident.Email,
                subject: "Package Pickup Notification"
            );
        }

        // Mark as picked up
        public async Task MarkAsPickedUpAsync(string trackingNumber)
        {
            var package = await _db.Packages.FirstOrDefaultAsync(p => p.TrackingNumber == trackingNumber);

            if (package == null) return;

            package.Status = "Picked Up";
            package.PickupDate = DateTime.Now;

            await _db.SaveChangesAsync();
        }

        // Add unknown package
        public void AddUnknownPackage(string nameOnPackage, string postalService)
        {
            var unknownPackage = new UnknownPackage
            {
                NameOnPackage = nameOnPackage,
                PostalService = postalService,
                DeliveredDate = DateTime.Now
            };

            _db.UnknownPackages.Add(unknownPackage);
            _db.SaveChanges();
        }

        // Search history by resident name/unit
        public List<Package> SearchHistory(string? name, int? unitNumber)
        {
            var query = _db.Packages.Include(p => p.Resident).AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Resident.FullName.Contains(name));

            if (unitNumber.HasValue)
                query = query.Where(p => p.Resident.UnitNumber == unitNumber.Value);

            return query.ToList();
        }
    }
}
