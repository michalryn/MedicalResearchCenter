using System.ComponentModel.DataAnnotations;

namespace MedicalResearchCenter.Data.DTOs.LabReferral
{
    public class CreateLabReferralDTO
    {
        public DateTime ScheduledDate { get; set; }
        public bool Consent { get; set; }
        public int ResearchProjectId { get; set; }
        public int PatientId { get; set; }
        public IEnumerable<int> LabTests { get; set; }
    }
}
