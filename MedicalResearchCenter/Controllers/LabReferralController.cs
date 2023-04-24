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

        #endregion

    }
}
