using MedicalResearchCenter.Data.DTOs.LabReferral;
using MedicalResearchCenter.Data.DTOs.Patient;
using MedicalResearchCenter.Services;
using MedicalResearchCenter.ViewModels.LabReferral;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalResearchCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabReferralController : ControllerBase
    {
        #region Properties

        private readonly LabReferralService _labReferralService;

        #endregion

        #region Constructors

        public LabReferralController(LabReferralService labReferralService)
        {
            _labReferralService = labReferralService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronous method for creating a lab referral
        /// </summary>
        /// <param name="newLabReferral">Object containing information about the lab referral</param>
        /// <returns>Success message</returns>
        /// <response code="201">Success message</response>
        /// <response code="404">Error message</response>
        /// <response code="409">Error message</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("AddLabReferralAsync")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> AddLabReferralAsync(NewLabReferralViewModel newLabReferral)
        {
            CreateLabReferralDTO dto = new CreateLabReferralDTO()
            {
                ScheduledDate = newLabReferral.ScheduledDate,
                Consent = false,
                ResearchProjectId = newLabReferral.ResearchProjectId,
                PatientId = newLabReferral.PatientId,
                LabTests = newLabReferral.LabTests
            };

            var result = await _labReferralService.AddLabReferralAsync(dto);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode, result.Message);
        }

        /// <summary>
        /// Asynchronous method confirming patients consent for participation in the lab tests
        /// </summary>
        /// <param name="labReferralId">Id of the lab referral</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpPut]
        [Route("ConfirmPatientConsentAsync/{labReferralId}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> ConfirmPatientConsentAsync(int labReferralId)
        {
            var result = await _labReferralService.ConfirmPatientConsentAsync(labReferralId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        /// <summary>
        /// Asynchronous method for loading a labreferral specified by an id
        /// </summary>
        /// <param name="labReferralId">Id of the lab referral</param>
        /// <returns>Object containing information about the lab referral</returns>
        /// <response code="200">Object containing information about the lab referral</response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpGet]
        [Route("GetLabReferral/{labReferralId}")]
        [ProducesResponseType(typeof(ReadLabReferralDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetLabReferralAsync(int labReferralId)
        {
            var result = await _labReferralService.GetLabReferralAsync(labReferralId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            ReadLabReferralDTO labReferral = (ReadLabReferralDTO)result.Result;

            return Ok(labReferral);
        }

        /// <summary>
        /// Asynchronous method for loading all lab referrals
        /// </summary>
        /// <remarks>
        /// The number of the first page is 1. Both "PageNumber" and "PageSize" have to be greater or equal to 1.
        /// 
        /// Both "ResearchProjectId" and "PatientId" are optional and used for filtering the results.
        /// </remarks>
        /// <param name="paging">Object containing information about paging and filtering</param>
        /// <returns>Object containing a list of lab referrals along with information about paging</returns>
        /// <response code="200">Object containing a list of lab referrals along with information about paging</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("GetLabReferralsAsync")]
        [ProducesResponseType(typeof(GetLabReferralsResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetLabReferralsAsync(GetLabReferralsViewModel paging)
        {
            GetLabReferralsDTO dto = new GetLabReferralsDTO()
            {
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize,
                PatientId = paging.PatientId,
                ResearchProjectId = paging.ResearchProjectId
            };

            var result = await _labReferralService.GetLabReferralsAsync(dto);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            GetLabReferralsResponseDTO labReferrals = (GetLabReferralsResponseDTO)result.Result;

            return Ok(labReferrals);
        }

        /// <summary>
        /// Asynchronous method for deleting a lab referral
        /// </summary>
        /// <remarks>
        /// Deleting a lab referral also removes all lab tests results affiliated with the lab referral
        /// </remarks>
        /// <param name="labReferralId">Id of the lab referral</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpDelete]
        [Route("DeleteLabReferralAsync/{labReferralId}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> DeleteLabReferralAsync(int labReferralId)
        {
            var result = await _labReferralService.DeleteLabReferralAsync(labReferralId);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }
        #endregion

    }
}
