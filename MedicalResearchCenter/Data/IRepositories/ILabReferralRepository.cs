using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface ILabReferralRepository
    {
        Task AddLabReferralAsync(LabReferral labReferral);
        Task<LabReferral> GetLabReferralAsync(int id);
        IQueryable<LabReferral> GetLabReferrals();
        Task DeleteLabReferralAsync(LabReferral labReferral);
        Task UpdateLabReferralAsync(LabReferral labReferral);
    }
}
