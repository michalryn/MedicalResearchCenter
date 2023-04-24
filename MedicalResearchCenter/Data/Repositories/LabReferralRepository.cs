using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using MedicalResearchCenter.Migrations;
using Microsoft.EntityFrameworkCore;

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

        public async Task<LabReferral> GetLabReferralAsync(int id)
        {
            var result = await DataContext.LabReferrals
                .SingleOrDefaultAsync(l =>  l.Id == id);

            return result;
        }

        public async Task UpdateLabReferralAsync(LabReferral labReferral)
        {
            await UpdateAndSaveChangesAsync(labReferral);
        }
        #endregion

    }
}
