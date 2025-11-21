using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    [Table("Residents")]
    public class Resident
    {
        [Key]
        [Column("id")]
        public int ResidentID { get; set; }

        [Required]
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("unit_number")]
        public int UnitNumber { get; set; }
    }
}
