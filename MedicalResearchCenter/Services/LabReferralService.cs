using MedicalResearchCenter.Data.DTOs.LabReferral;
using MedicalResearchCenter.Data.DTOs.PatientTest;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using X.PagedList;

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
                bool projectExists = await _researchProjectRepo.ExistsAsync(dto.ResearchProjectId);

                if (!projectExists)
                {
                    return CreateFailureResponse(404, "Research project with such id was not found");
                }

                bool isPatientAssigned = await _researchProjectRepo.IsPatientAssignedAsync(dto.ResearchProjectId, dto.PatientId);

                if (!isPatientAssigned)
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

        public async Task<ServiceResponseDTO> GetLabReferralAsync(int labReferralId)
        {
            try
            {
                LabReferral labReferral = await _labReferralRepo.GetLabReferralAsync(labReferralId);

                if (labReferral == null)
                {
                    return CreateFailureResponse(404, "Referral with such id was not found");
                }

                ReadLabReferralDTO dto = new ReadLabReferralDTO()
                {
                    LabReferralId = labReferralId,
                    PatientId = labReferral.PatientId,
                    ResearchProjectId = labReferral.ResearchProjectId,
                    ScheduledDate = labReferral.ScheduledDate,
                    Consent = labReferral.Consent,
                    FirstName = labReferral.Patient.FirstName,
                    LastName = labReferral.Patient.LastName,
                    Pesel = labReferral.Patient.Pesel,
                    DateOfBirth = labReferral.Patient.DateOfBirth,
                    Gender = labReferral.Patient.Gender
                };

                dto.PatientTests = labReferral.PatientTests.Select(t => new ReadPatientTestDTO()
                {
                    LabReferralId = labReferralId,
                    LabTestId = t.LabTestId,
                    Result = t.Result,
                    Name = t.LabTest.Name,
                    Unit = t.LabTest.Unit,
                    NormFrom = t.LabTest.NormFrom,
                    NormTo = t.LabTest.NormTo
                });

                return CreateSuccessResponse(200, "Lab referral retrieved successfully", dto);
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrieving the lab referral");
            }
        }

        public async Task<ServiceResponseDTO> GetLabReferralsAsync(GetLabReferralsDTO dto)
        {
            try
            {
                IQueryable<LabReferral> labReferrals = _labReferralRepo.GetLabReferrals();

                if (dto.PatientId != null)
                {
                    labReferrals = labReferrals.Where(r => r.PatientId == dto.PatientId);
                }

                if (dto.ResearchProjectId != null)
                {
                    labReferrals = labReferrals.Where(r => r.ResearchProjectId == dto.ResearchProjectId);
                }

                GetLabReferralsResponseDTO response = new GetLabReferralsResponseDTO();

                response.TotalCount = labReferrals.Count();
                response.PageNumber = dto.PageNumber;
                response.PageSize = dto.PageSize;

                response.LabReferrals = await labReferrals.Select(r => new ReadLabReferralDTO()
                {
                    LabReferralId = r.Id,
                    PatientId = r.PatientId,
                    ResearchProjectId = r.ResearchProjectId,
                    ScheduledDate = r.ScheduledDate,
                    Consent = r.Consent,
                    FirstName = r.Patient.FirstName,
                    LastName = r.Patient.LastName,
                    Pesel = r.Patient.Pesel,
                    DateOfBirth = r.Patient.DateOfBirth,
                    Gender = r.Patient.Gender
                }).ToPagedListAsync(dto.PageNumber, dto.PageSize);

                return CreateSuccessResponse(200, "Lab referrals retrieved successfully", response);
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrieving lab referrals");
            }
        }

        public async Task<ServiceResponseDTO> DeleteLabReferralAsync(int labReferralId)
        {
            try
            {
                LabReferral labReferral = await _labReferralRepo.GetLabReferralAsync(labReferralId);

                if (labReferral == null)
                {
                    return CreateFailureResponse(404, "Lab refferal with such id was not found");
                }

                await _labReferralRepo.DeleteLabReferralAsync(labReferral);

                return CreateSuccessResponse(204, "");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while deleting lab referral");
            }
        }

        public async Task<ServiceResponseDTO> ConfirmPatientConsentAsync(int labReferralId)
        {
            try
            {
                LabReferral labReferral = await _labReferralRepo.GetLabReferralAsync(labReferralId);

                if (labReferral == null)
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
