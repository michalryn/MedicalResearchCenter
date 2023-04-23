using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedicalResearchCenter.ViewModels.Patient
{
    public class GetPatientsViewModel
    {
        [Required]
        [Range(1, 50)]
        [DefaultValue(1)]
        public int PageSize { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [DefaultValue(1)]
        public int PageNumber { get; set; }
    }
}
