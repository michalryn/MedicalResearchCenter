using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface ILabReferralRepository
    {
        Task AddLabReferralAsync(LabReferral labReferral);
        Task<LabReferral> GetLabReferralAsync(int id);
        Task UpdateLabReferralAsync(LabReferral labReferral);
    }
}
