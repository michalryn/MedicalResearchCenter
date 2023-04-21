using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

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

        public async Task<Patient> GetPatientAsync(int id)
        {
            var result = await DataContext.Patients
                .SingleOrDefaultAsync(p => p.Id == id);

            return result;
        }

        public async Task DeletePatientAsync(Patient patient)
        {
            Remove(patient);
            await SaveChangesAsync();
        }

    }
}
