using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface IPatientRepository
    {
        Task<Patient> AddPatientAsync(Patient patient);
    }
}
