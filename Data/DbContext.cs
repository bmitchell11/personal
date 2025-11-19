using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;

namespace FinalProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Resident> Residents { get; set; }
        public DbSet<Staff> StaffLogin { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<UnknownPackage> UnknownPackages { get; set; }
    }
}