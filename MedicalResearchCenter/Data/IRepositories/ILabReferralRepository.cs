using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface ILabReferralRepository
    {
        Task AddLabReferralAsync(LabReferral labReferral);
    }
}
