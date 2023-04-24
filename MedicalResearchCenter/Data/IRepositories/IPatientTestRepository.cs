using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface IPatientTestRepository
    {
        Task<bool> IsLabTestUsedAsync(int labTestId);
        Task<List<PatientTest>> GetPatientTestsAsync(int referralId);
        Task<PatientTest> GetPatientTestAsync(int labReferralId, int labTestId);
        Task UpdatePatientTestsAsync(IEnumerable<PatientTest> patientTests);
        Task DeletePatientTestAsync(PatientTest patientTest);
    }
}
