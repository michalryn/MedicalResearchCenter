using MedicalResearchCenter.Data.DTOs.LabReferral;
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

        [HttpPost]
        [Route("AddLabReferralAsync")]
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

        [HttpPut]
        [Route("ConfirmPatientConsentAsync/{labReferralId}")]
        public async Task<IActionResult> ConfirmPatientConsentAsync(int labReferralId)
        {
            var result = await _labReferralService.ConfirmPatientConsentAsync(labReferralId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        [HttpGet]
        [Route("GetLabReferral/{labReferralId}")]
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

        [HttpPost]
        [Route("GetLabReferralsAsync")]
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

        [HttpDelete]
        [Route("DeleteLabReferralAsync/{labReferralId}")]
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
