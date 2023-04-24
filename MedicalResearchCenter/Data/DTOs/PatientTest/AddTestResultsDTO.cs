namespace MedicalResearchCenter.Data.DTOs.PatientTest
{
    public class AddTestResultsDTO
    {
        public int LabReferralId { get; set; }
        
        public IEnumerable<AddTestResultDTO> Results { get; set; }
    }
}
