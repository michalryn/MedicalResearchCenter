using MedicalResearchCenter.Data.DTOs.ResearchProject;

namespace MedicalResearchCenter.Data.DTOs.Patient
{
    public class GetPatientsResponseDTO
    {
        public IEnumerable<ReadPatientToListDTO> Patients { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
