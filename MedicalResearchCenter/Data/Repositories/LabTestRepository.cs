using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearchCenter.Data.Repositories
{
    public class LabTestRepository : BaseRepository<LabTest>, ILabTestRepository
    {
        #region Constructors
        public LabTestRepository(DataContext context) : base(context) { }

        #endregion

        #region Methods

        public async Task<LabTest> AddLabTestAsync(LabTest labTest)
        {
            await AddAndSaveChangesAsync(labTest);

            await DataContext
                .Entry(labTest)
                .ReloadAsync();

            return labTest;
        }

        public async Task<LabTest> GetLabTestAsync(int id)
        {
            var result = await DataContext.LabTests
                .SingleOrDefaultAsync(l => l.Id == id);

            return result;
        }

        public async Task UpdateLabTestAsync(LabTest labTest)
        {
            await UpdateAndSaveChangesAsync(labTest);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            bool result = await DataContext.LabTests
                .AnyAsync(l => l.Name == name);

            return result;
        }

        #endregion
    }
}
