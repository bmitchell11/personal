using System;

namespace FinalProject.Models
{
    public class Package
    {
        public int PackageID { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public int? ResidentID { get; set; }
        public Resident? Resident { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
