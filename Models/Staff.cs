using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    [Table("StaffLogin")]
    public class Staff
    {
        [Key]
        public string staff_username { get; set; }  = string.Empty;

        public string staff_password { get; set; }  = string.Empty;
    }
}
