namespace MedicalResearchCenter.Data.DTOs.ResearchProject
{
    public class GetRPsResponseDTO
    {
        public IEnumerable<ReadRPDTO> ResearchProjects { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
