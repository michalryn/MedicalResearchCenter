using MedicalResearchCenter.Data.DTOs.Patient;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;

namespace MedicalResearchCenter.Services
{
    public class PatientService : BaseService
    {
        #region Properties

        private readonly IPatientRepository _patientRepo;

        #endregion

        #region Constructors

        public PatientService(IPatientRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponseDTO> AddPatientAsync(CreatePatientDTO dto)
        {
            try
            {
                Patient patient = new Patient()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Pesel = dto.Pesel,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    Address = dto.Address
                };

                patient = await _patientRepo.AddPatientAsync(patient);

                return CreateSuccessResponse(201, "Patient created successfully");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while creating patient");
            }
        }

        #endregion
    }
}
