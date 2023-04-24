using MedicalResearchCenter.Data.DTOs.ResearchProject;

namespace MedicalResearchCenter.Data.DTOs.LabTest
{
    public class GetLabTestsResponseDTO
    {
        public IEnumerable<ReadLabTestDTO> LabTests { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
