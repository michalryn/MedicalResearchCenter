namespace MedicalResearchCenter.Data.DTOs.Patient
{
    public class GetPatientsDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? ProjectId { get; set; }
        public bool Assigned { get; set; }
    }
}
