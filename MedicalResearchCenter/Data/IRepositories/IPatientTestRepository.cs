namespace MedicalResearchCenter.Data.IRepositories
{
    public interface IPatientTestRepository
    {
        Task<bool> IsLabTestUsedAsync(int labTestId);
    }
}
