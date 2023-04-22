using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface IResearchProjectRepository
    {
        Task<ResearchProject> AddResearchProjectAsync(ResearchProject project);
        Task<ResearchProject> GetResearchProjectAsync(int id);
        Task DeleteResearchProjectAsync(ResearchProject project);
        Task UpdateResearchProjectAsync(ResearchProject project);
    }
}
