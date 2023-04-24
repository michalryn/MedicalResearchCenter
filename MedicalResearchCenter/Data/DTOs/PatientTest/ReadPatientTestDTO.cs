namespace MedicalResearchCenter.Data.DTOs.PatientTest
{
    public class ReadPatientTestDTO
    {
        public int LabTestId { get; set; }
        public int LabReferralId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double NormFrom { get; set; }
        public double NormTo { get; set;}
        public double Result { get; set; }
    }
}
