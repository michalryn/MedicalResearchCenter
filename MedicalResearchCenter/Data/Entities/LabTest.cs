using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalResearchCenter.Data.Entities
{
    [Table("LabTests")]
    public class LabTest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string Unit { get; set; }

        [Required]
        public double NormFrom { get; set; }

        [Required]
        public double NormTo { get; set; }
    }
}
