namespace MedicalResearchCenter.Data.DTOs.ResearchProject
{
    public class UpdateRPDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Manager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
