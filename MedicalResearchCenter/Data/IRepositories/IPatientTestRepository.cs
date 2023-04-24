using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface IPatientTestRepository
    {
        Task<bool> IsLabTestUsedAsync(int labTestId);
        Task<List<PatientTest>> GetPatientTestsAsync(int referralId);
        Task UpdatePatientTestsAsync(IEnumerable<PatientTest> patientTests);
    }
}
