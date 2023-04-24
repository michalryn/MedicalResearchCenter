﻿using MedicalResearchCenter.Data.DTOs.LabTest;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using MedicalResearchCenter.ViewModels.LabTest;
using Microsoft.AspNetCore.Mvc;

namespace MedicalResearchCenter.Services
{
    public class LabTestService : BaseService
    {
        #region Properties

        private readonly ILabTestRepository _labTestRepo;

        #endregion

        #region Constructors

        public LabTestService(ILabTestRepository labTestRepo)
        {
            _labTestRepo = labTestRepo;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponseDTO> AddLabTestAsync(CreateLabTestDTO dto)
        {
            try
            {
                bool exists = await _labTestRepo.ExistsAsync(dto.Name);

                if(exists)
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
            catch(Exception ex)
            {
                return CreateFailureResponse(500, "Error while creating the lab test");
            }
        }

        public async Task<ServiceResponseDTO> GetLabTestAsync(int labTestId)
        {
            try
            {
                LabTest labTest = await _labTestRepo.GetLabTestAsync(labTestId);

                if(labTest == null)
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
            catch(Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrieving the lab test");
            }
        }

        public async Task<ServiceResponseDTO> UpdateLabTestAsync(UpdateLabTestDTO dto)
        {
            try
            {
                LabTest labTest = await _labTestRepo.GetLabTestAsync(dto.Id);

                if(labTest == null)
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

        #endregion
    }
}