using MedicalResearchCenter.Data.DTOs.PatientTest;
using MedicalResearchCenter.Services;
using MedicalResearchCenter.ViewModels.PatientTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalResearchCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientTestController : ControllerBase
    {
        #region Properties

        private readonly PatientTestService _patientTestService;

        #endregion

        #region Constructors

        public PatientTestController(PatientTestService patientTestService)
        {
            _patientTestService = patientTestService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("UpdateTestResults/{labReferralId}")]
        public async Task<IActionResult> UpdateTestResultsAsync(int labReferralId, AddTestResultsViewModel testResults)
        {
            AddTestResultsDTO dto = new AddTestResultsDTO()
            {
                LabReferralId = labReferralId,
                Results = testResults.TestResults
            };

            var result = await _patientTestService.AddTestResultsAsync(dto);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        [HttpDelete]
        [Route("DeleteTestResultAsync")]
        public async Task<IActionResult> DeleteTestResultAsync(int labReferralId, int labTestId)
        {
            var result = await _patientTestService.DeleteTestResultAsync(labReferralId, labTestId);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        #endregion

    }
}
