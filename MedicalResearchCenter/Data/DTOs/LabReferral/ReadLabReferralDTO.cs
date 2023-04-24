using MedicalResearchCenter.Data.DTOs.PatientTest;

namespace MedicalResearchCenter.Data.DTOs.LabReferral
{
    public class ReadLabReferralDTO
    {
        public int LabReferralId { get; set; }
        public int PatientId { get; set; }
        public int ResearchProjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime ScheduledDate { get; set; }
        public bool Consent { get; set; }
        public IEnumerable<ReadPatientTestDTO> PatientTests { get; set; }
    }
}
