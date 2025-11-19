using System.ComponentModel.DataAnnotations;
namespace FinalProject.Models;

public class UnknownPackage
{
    public int UnknownPackageID { get; set; }
    public string NameOnPackage { get; set; } = string.Empty;
    public string PostalService { get; set; } = string.Empty;
    public DateTime DeliveredDate { get; set; }
}
