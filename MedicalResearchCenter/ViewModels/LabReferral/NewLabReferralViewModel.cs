using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.ViewModels.LabReferral
{
    public class NewLabReferralViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public int ResearchProjectId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<int> LabTests { get; set; }
    }
}
