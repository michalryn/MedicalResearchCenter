using MedicalResearchCenter.Data.DTOs.LabTest;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using MedicalResearchCenter.ViewModels.LabTest;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MedicalResearchCenter.Services
{
    public class LabTestService : BaseService
    {
        #region Properties

        private readonly ILabTestRepository _labTestRepo;
        private readonly IPatientTestRepository _patientTestRepo;

        #endregion

        #region Constructors

        public LabTestService(ILabTestRepository labTestRepo, IPatientTestRepository patientTestRepo)
        {
            _labTestRepo = labTestRepo;
            _patientTestRepo = patientTestRepo;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponseDTO> AddLabTestAsync(CreateLabTestDTO dto)
        {
            try
            {
                bool exists = await _labTestRepo.ExistsAsync(dto.Name);

                if (exists)
                {
                    return CreateFailureResponse(409, "Lab test with such name already exists");
                }

                LabTest labTest = new LabTest()
                {
                    Name = dto.Name,
                    Unit = dto.Unit,
                    NormFrom = dto.NormFrom,
                    NormTo = dto.NormTo
                };

                labTest = await _labTestRepo.AddLabTestAsync(labTest);

                ReadLabTestDTO newLabTest = new ReadLabTestDTO()
                {
                    Id = labTest.Id,
                    Name = labTest.Name,
                    Unit = labTest.Unit,
                    NormFrom = labTest.NormFrom,
                    NormTo = labTest.NormTo
                };

                return CreateSuccessResponse(201, "Lab test created successfully", newLabTest);
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while creating the lab test");
            }
        }

        public async Task<ServiceResponseDTO> GetLabTestAsync(int labTestId)
        {
            try
            {
                LabTest labTest = await _labTestRepo.GetLabTestAsync(labTestId);

                if (labTest == null)
                {
                    return CreateFailureResponse(404, "Lab test with such id was not found");
                }

                ReadLabTestDTO dto = new ReadLabTestDTO()
                {
                    Id = labTest.Id,
                    Name = labTest.Name,
                    Unit = labTest.Unit,
                    NormFrom = labTest.NormFrom,
                    NormTo = labTest.NormTo
                };

                return CreateSuccessResponse(200, "Lab test retrieved successfully", dto);
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrieving the lab test");
            }
        }

        public async Task<ServiceResponseDTO> GetLabTestsAsync(GetLabTestsDTO dto)
        {
            try
            {
                IQueryable<LabTest> labTests = _labTestRepo.GetLabTests();

                labTests = labTests.OrderBy(t => t.Name);

                GetLabTestsResponseDTO response = new GetLabTestsResponseDTO();
                response.TotalCount = labTests.Count();
                response.PageNumber = dto.PageNumber;
                response.PageSize = dto.PageSize;

                response.LabTests = await labTests
                    .Select(t => new ReadLabTestDTO()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Unit = t.Unit,
                        NormFrom = t.NormFrom,
                        NormTo = t.NormTo
                    }).ToPagedListAsync(dto.PageNumber, dto.PageSize);

                return CreateSuccessResponse(200, "Lab tests retrieved successfully", response);
            }
            catch(Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrieving lab tests");
            }
        }

        public async Task<ServiceResponseDTO> UpdateLabTestAsync(UpdateLabTestDTO dto)
        {
            try
            {
                bool exists = await _labTestRepo.ExistsAsync(dto.Name);

                if(exists)
                {
                    return CreateFailureResponse(409, "Lab test with such name already exists");
                }

                LabTest labTest = await _labTestRepo.GetLabTestAsync(dto.Id);

                if (labTest == null)
                {
                    return CreateFailureResponse(404, "Lab test with such id was not found");
                }

                labTest.Name = dto.Name;
                labTest.Unit = dto.Unit;
                labTest.NormFrom = dto.NormFrom;
                labTest.NormTo = dto.NormTo;

                await _labTestRepo.UpdateLabTestAsync(labTest);

                return CreateSuccessResponse(204, "");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrieving the lab test");
            }
        }

        public async Task<ServiceResponseDTO> DeleteLabTestAsync(int labTestId)
        {
            try
            {
                LabTest labTest = await _labTestRepo.GetLabTestAsync(labTestId);

                if (labTest == null)
                {
                    return CreateFailureResponse(404, "Lab test with such id was not found");
                }

                bool isUsed = await _patientTestRepo.IsLabTestUsedAsync(labTest.Id);

                if (isUsed)
                {
                    return CreateFailureResponse(409, "Lab test is used in patient tests, DELETE operation is forbidden");
                }

                await _labTestRepo.DeleteLabTestAsync(labTest);

                return CreateSuccessResponse(204, "");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while deleting the lab test");
            }
        }

        #endregion
    }
}
