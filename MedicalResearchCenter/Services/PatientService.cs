using MedicalResearchCenter.Data.DTOs.Patient;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

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
                    Region = dto.Region,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    Street = dto.Street,
                    UnitNumber = dto.UnitNumber
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
                    Region = patient.Region,
                    City = patient.City,
                    PostalCode = patient.PostalCode,
                    Street = patient.Street,
                    UnitNumber = patient.UnitNumber
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
                    Region = patient.Region,
                    City = patient.City,
                    PostalCode = patient.PostalCode,
                    Street = patient.Street,
                    UnitNumber = patient.UnitNumber
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

        public async Task<ServiceResponseDTO> UpdatePatientAsync(UpdatePatientDTO dto)
        {
            try
            {
                Patient oldPatient = await _patientRepo.GetPatientAsync(dto.Id);

                if(oldPatient == null)
                {
                    return CreateFailureResponse(404, "Paitent with such id was not found");
                }

                oldPatient.FirstName = dto.FirstName;
                oldPatient.LastName = dto.LastName;
                oldPatient.Pesel = dto.Pesel;
                oldPatient.Gender = dto.Gender;
                oldPatient.DateOfBirth = dto.DateOfBirth;
                oldPatient.PhoneNumber = dto.PhoneNumber;
                oldPatient.Email = dto.Email;
                oldPatient.Region = dto.Region;
                oldPatient.City = dto.City;
                oldPatient.Street = dto.Street;
                oldPatient.UnitNumber = dto.UnitNumber;

                await _patientRepo.UpdatePatientAsync(oldPatient);

                return CreateSuccessResponse(204, "");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while updating patient");
            }
        }

        public async Task<ServiceResponseDTO> GetPatientsAsync(GetPatientsDTO dto)
        {
            try
            {
                IQueryable<Patient> patients = _patientRepo.GetPatientsAsync().Include(p => p.ResearchProjects);

                if(dto.ProjectId != null && dto.Assigned == true)
                {
                    patients = patients.Where(p => p.ResearchProjects.Any(rp => rp.Id == dto.ProjectId));
                }

                if (dto.ProjectId != null && dto.Assigned == false)
                {
                    patients = patients.Where(p => p.ResearchProjects.All(rp => rp.Id != dto.ProjectId));
                }

                patients = patients.OrderBy(p => p.LastName);

                GetPatientsResponseDTO response = new GetPatientsResponseDTO();
                response.TotalCount = patients.Count();
                response.PageNumber = dto.PageNumber;
                response.PageSize = dto.PageSize;

                response.Patients = await patients
                    .Select(p => new ReadPatientToListDTO()
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Pesel = p.Pesel,
                        Gender = p.Gender,
                        DateOfBirth = p.DateOfBirth
                    }).ToPagedListAsync(dto.PageNumber, dto.PageSize);

                return CreateSuccessResponse(200, "Patients retrieved successfully", response);
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrievieng patients");
            }
        }
        #endregion
    }
}
