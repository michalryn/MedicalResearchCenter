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

        #endregion

        #region Constructors

        public LabReferralService(ILabReferralRepository labReferralRepo)
        {
            _labReferralRepo = labReferralRepo;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponseDTO> AddLabReferralAsync(CreateLabReferralDTO dto)
        {
            try
            {
                ICollection<LabReferralLabTest> labTests = dto.LabTests.Select(l => new LabReferralLabTest()
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
                    LabTests = labTests
                };

                await _labReferralRepo.AddLabReferralAsync(labReferral);

                return CreateSuccessResponse(201, "Lab referral created successfully");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while creating the lab referral");
            }
        }

        #endregion

    }
}
