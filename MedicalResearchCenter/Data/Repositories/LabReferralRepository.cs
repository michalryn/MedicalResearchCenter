using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using MedicalResearchCenter.Migrations;

namespace MedicalResearchCenter.Data.Repositories
{
    public class LabReferralRepository : BaseRepository<LabReferral>, ILabReferralRepository
    {
        #region Constructors

        public LabReferralRepository(DataContext context) : base(context) { }

        #endregion

        #region Methods

        public async Task AddLabReferralAsync(LabReferral labReferral)
        {
            await AddAndSaveChangesAsync(labReferral);
        }

        #endregion

    }
}
