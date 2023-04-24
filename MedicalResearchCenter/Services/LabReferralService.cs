using MedicalResearchCenter.Data.DTOs.LabReferral;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;

namespace MedicalResearchCenter.Services
{
    public class LabReferralService : BaseService
    {
        #region Properties

        private readonly ILabReferralRepository _labReferralRepo;
        private readonly IResearchProjectRepository _researchProjectRepo;

        #endregion

        #region Constructors

        public LabReferralService(ILabReferralRepository labReferralRepo, IResearchProjectRepository researchProjectRepo)
        {
            _labReferralRepo = labReferralRepo;
            _researchProjectRepo = researchProjectRepo;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponseDTO> AddLabReferralAsync(CreateLabReferralDTO dto)
        {
            try
            {
                bool isPatientAssigned = await _researchProjectRepo.IsPatientAssignedAsync(dto.ResearchProjectId, dto.PatientId);

                if(!isPatientAssigned)
                {
                    return CreateFailureResponse(409, "Patient is not assigned to this research project");
                }

                ICollection<PatientTest> labTests = dto.LabTests.Select(l => new PatientTest()
                {
                    LabTestId = l,
                    LabReferralId = dto.ResearchProjectId
                }).ToList();

                LabReferral labReferral = new LabReferral()
                {
                    ScheduledDate = dto.ScheduledDate,
                    Consent = dto.Consent,
                    ResearchProjectId = dto.ResearchProjectId,
                    PatientId = dto.PatientId,
                    PatientTests = labTests
                };

                await _labReferralRepo.AddLabReferralAsync(labReferral);

                return CreateSuccessResponse(201, "Lab referral created successfully");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while creating the lab referral");
            }
        }

        public async Task<ServiceResponseDTO> ConfirmPatientConsentAsync(int labReferralId)
        {
            try
            {
                LabReferral labReferral = await _labReferralRepo.GetLabReferralAsync(labReferralId);

                if(labReferral == null)
                {
                    return CreateFailureResponse(404, "Lab referral with such id was not found");
                }

                labReferral.Consent = true;

                await _labReferralRepo.UpdateLabReferralAsync(labReferral);

                return CreateSuccessResponse(204, "");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while modifying the consent");
            }
        }
        #endregion

    }
}
