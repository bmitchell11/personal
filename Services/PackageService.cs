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
        public async Task AddPackageAsync(int residentID, string postalService)
        {
            var resident = await _db.Residents.FindAsync(residentID);

            if (resident == null) return;

            var package = new Package
            {
                ResidentID = residentID,
                PostalService = postalService,
                ReceivedDate = DateTime.Now,
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
        public List<Package> GetPendingPackages()
        {
            return _db.Packages
                    .Include(p => p.Resident)
                    .Where(p => p.Status == false) // false = not picked up
                    .ToList();
        }
        public async Task<bool> MarkAsPickedUpAsync(int residentID, string postalService)
        {
            // Find the pending package for this resident
            var package = await _db.Packages
                .FirstOrDefaultAsync(p => p.ResidentID == residentID 
                                    && p.PostalService == postalService 
                                    && p.Status == false); // only pending packages

            if (package == null) 
                return false;

            package.PickupDate = DateTime.Now;
            package.Status = true; // mark as picked up

            await _db.SaveChangesAsync();
            return true;
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
    }
}
