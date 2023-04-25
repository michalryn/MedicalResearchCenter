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

        /// <summary>
        /// Asynchronous method for updating a patients tests results assigned to a lab referral specified by an id
        /// </summary>
        /// <param name="labReferralId">Id of the lab referral</param>
        /// <param name="testResults">Object containig new information about patients tests results</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("UpdateTestResults/{labReferralId}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
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

        /// <summary>
        /// Asynchronous method for deleting a patients test result assigned to a lab referral specified an id
        /// </summary>
        /// <param name="labReferralId">Id of the lab referral</param>
        /// <param name="labTestId">Id of the lab test</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpDelete]
        [Route("DeleteTestResultAsync")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
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
