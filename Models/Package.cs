using System;

namespace FinalProject.Models
{
    public class Package
    {
        public int PackageID { get; set; }
        public string PostalService { get; set; } = string.Empty;
        public int? ResidentID { get; set; }
        public Resident? Resident { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public bool Status { get; set; } = false;
    }
}
