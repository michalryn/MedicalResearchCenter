using MedicalResearchCenter.Data.DTOs.PatientTest;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;

namespace MedicalResearchCenter.Services
{
    public class PatientTestService : BaseService
    {
        #region Properties

        private readonly IPatientTestRepository _patientTestRepo;

        #endregion

        #region Constructors

        public PatientTestService(IPatientTestRepository patientTestRepo)
        {
            _patientTestRepo = patientTestRepo;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponseDTO> AddTestResultsAsync(AddTestResultsDTO testResults)
        {
            try
            {
                ICollection<PatientTest> patientTests = await _patientTestRepo.GetPatientTestsAsync(testResults.LabReferralId);

                if(patientTests.Count() == 0)
                {
                    return CreateFailureResponse(404, "No lab test where found for that lab referral");
                }

                Dictionary<int, AddTestResultDTO> testsDictionary = testResults.Results.ToDictionary(r => r.LabTestId);
                //patientTests.OrderBy(t => t.LabTestId);

                foreach(PatientTest patientTest in patientTests)
                {
                    patientTest.Result = testsDictionary[patientTest.LabTestId].Result;
                }

                await _patientTestRepo.UpdatePatientTestsAsync(patientTests);

                return CreateSuccessResponse(204, "");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while adding patient test results");
            }
        }

        #endregion

    }
}
