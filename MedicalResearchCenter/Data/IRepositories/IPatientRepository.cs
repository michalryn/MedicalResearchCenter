using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface IPatientRepository
    {
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient> GetPatientAsync(int id);
        Task DeletePatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        IQueryable<Patient> GetPatientsAsync();
    }
}
