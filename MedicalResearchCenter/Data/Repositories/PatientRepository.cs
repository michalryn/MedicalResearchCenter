using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;

namespace MedicalResearchCenter.Data.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DataContext context) : base(context) { }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            await AddAndSaveChangesAsync(patient);

            await DataContext
                .Entry(patient)
                .ReloadAsync();

            return patient;
        }

    }
}
