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

        #endregion

    }
}
