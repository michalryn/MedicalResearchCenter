﻿using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface IResearchProjectRepository
    {
        Task<ResearchProject> AddResearchProjectAsync(ResearchProject project);
        Task<ResearchProject> GetResearchProjectAsync(int id);
        Task<ResearchProject> GetResearchProjectWithPatientsAsync(int id);
        Task DeleteResearchProjectAsync(ResearchProject project);
        Task UpdateResearchProjectAsync(ResearchProject project);
        IQueryable<ResearchProject> GetResearchProjects();
        Task<bool> IsPatientAssignedAsync(int projectId, int patientId);
        Task<bool> ExistsAsync(int projectId);
    }
}
