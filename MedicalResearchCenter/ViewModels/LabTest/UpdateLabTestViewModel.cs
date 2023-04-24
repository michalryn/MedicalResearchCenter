using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.ViewModels.LabTest
{
    public class UpdateLabTestViewModel
    {
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
