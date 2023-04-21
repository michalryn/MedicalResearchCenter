﻿using MedicalResearchCenter.Data.DTOs.Patient;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;

namespace MedicalResearchCenter.Services
{
    public class PatientService : BaseService
    {
        #region Properties

        private readonly IPatientRepository _patientRepo;

        #endregion

        #region Constructors

        public PatientService(IPatientRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponseDTO> AddPatientAsync(CreatePatientDTO dto)
        {
            try
            {
                Patient patient = new Patient()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Pesel = dto.Pesel,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    Address = dto.Address
                };

                patient = await _patientRepo.AddPatientAsync(patient);

                ReadPatientDTO newPatientDTO = new ReadPatientDTO()
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Pesel = patient.Pesel,
                    Gender = patient.Gender,
                    DateOfBirth = patient.DateOfBirth,
                    PhoneNumber = patient.PhoneNumber,
                    Email = patient.Email,
                    Address = patient.Address
                };

                return CreateSuccessResponse(201, "Patient created successfully", newPatientDTO);
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while creating patient");
            }
        }

        public async Task<ServiceResponseDTO> GetPatientAsync(int patientId)
        {
            try
            {
                Patient patient = await _patientRepo.GetPatientAsync(patientId);
                
                if(patient == null)
                {
                    return CreateFailureResponse(404, "Patient with such id was not found");
                }

                ReadPatientDTO dto = new ReadPatientDTO()
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Pesel = patient.Pesel,
                    Gender = patient.Gender,
                    DateOfBirth = patient.DateOfBirth,
                    PhoneNumber = patient.PhoneNumber,
                    Email = patient.Email,
                    Address = patient.Address
                };

                return CreateSuccessResponse(200, "Patient retrived successfully", dto);
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrieving the tutorchip");
            }
        }

        public async Task<ServiceResponseDTO> DeletePatientAsync(int patientId)
        {
            try
            {
                var patient = await _patientRepo.GetPatientAsync(patientId);

                if (patient == null)
                {
                    return CreateFailureResponse(404, "Patient with such id was not found");
                }

                await _patientRepo.DeletePatientAsync(patient);

                return CreateSuccessResponse(204, "");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while deleting the tutorship");
            }
        }

        #endregion
    }
}
