using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearchCenter.Data.Repositories
{
    public class PatientTestRepository : BaseRepository<PatientTest>, IPatientTestRepository
    {
        #region Constructors

        public PatientTestRepository(DataContext context) : base(context) { }

        #endregion

        #region Methods

        public async Task<bool> IsLabTestUsedAsync(int labTestId)
        {
            var result = await DataContext.PatientTests
                .AnyAsync(p => p.LabTestId == labTestId);

            return result;
        }

        public async Task<List<PatientTest>> GetPatientTestsAsync(int referralId)
        {
            var result = await DataContext.PatientTests
                .Include(t => t.LabTest)
                .Where(t => t.LabReferralId == referralId)
                .ToListAsync();

            return result;
        }

        public async Task UpdatePatientTestsAsync(IEnumerable<PatientTest> patientTests)
        {
            await UpdateRangeAndSaveChangesAsync(patientTests);
        }
        
        #endregion

    }
}
