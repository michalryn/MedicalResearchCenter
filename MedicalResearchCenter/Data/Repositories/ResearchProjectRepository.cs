using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearchCenter.Data.Repositories
{
    public class ResearchProjectRepository : BaseRepository<ResearchProject>, IResearchProjectRepository
    {

        #region Constructors

        public ResearchProjectRepository(DataContext context) : base(context) { }

        #endregion

        #region Methods

        public async Task<ResearchProject> AddResearchProjectAsync(ResearchProject project)
        {
            await AddAndSaveChangesAsync(project);

            await DataContext
                .Entry(project)
                .ReloadAsync();

            return project;
        }

        public async Task<ResearchProject> GetResearchProjectAsync(int id)
        {
            var result = await DataContext.ResearchProjects
                .SingleOrDefaultAsync(p => p.Id == id);

            return result;
        }

        public async Task<ResearchProject> GetResearchProjectWithPatientsAsync(int id)
        {
            var result = await DataContext.ResearchProjects
                .Include(p => p.Patients)
                .SingleOrDefaultAsync(p => p.Id == id);

            return result;
        }

        public async Task DeleteResearchProjectAsync(ResearchProject project)
        {
            Remove(project);
            await SaveChangesAsync();
        }

        public async Task UpdateResearchProjectAsync(ResearchProject project)
        {
            await UpdateAndSaveChangesAsync(project);
        }

        public IQueryable<ResearchProject> GetResearchProjects()
        {
            var result = DataContext.ResearchProjects
                .AsQueryable();

            return result;
        }

        public async Task<bool> IsPatientAssignedAsync(int projectId, int patientId)
        {
            var result = await DataContext.ResearchProjects
                .Include(r => r.Patients)
                .Where(r => r.Id == projectId && r.Patients.Any(p => p.Id == patientId))
                .AnyAsync();

            return result;
        }
        #endregion
    }
}
