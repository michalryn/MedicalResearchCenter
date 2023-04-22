using MedicalResearchCenter.Data.DTOs.Patient;
using MedicalResearchCenter.Services;
using MedicalResearchCenter.ViewModels.Patient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedicalResearchCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        #region Properties

        private readonly PatientService _patientService;

        #endregion

        #region Constructors

        public PatientController(PatientService patientService) 
        {
            _patientService = patientService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("AddPatientAsync")]
        public async Task<IActionResult> AddPatientAsync(NewPatientViewModel newPatient)
        {
            CreatePatientDTO dto = new CreatePatientDTO()
            {
                FirstName = newPatient.FirstName,
                LastName = newPatient.LastName,
                Pesel = newPatient.Pesel,
                Gender = newPatient.Gender,
                DateOfBirth = newPatient.DateOfBirth,
                PhoneNumber = newPatient.PhoneNumber,
                Email = newPatient.Email,
                Region = newPatient.Region,
                City = newPatient.City,
                PostalCode = newPatient.PostalCode,
                Street = newPatient.Street,
                UnitNumber = newPatient.UnitNumber
            };

            var result = await _patientService.AddPatientAsync(dto);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            ReadPatientDTO patientDTO = (ReadPatientDTO)result.Result;

            return CreatedAtRoute("GetPatientAsync", new { patientId = patientDTO.Id }, patientDTO);
        }

        [HttpGet]
        [Route("GetPatientAsync/{patientId}", Name = "GetPatientAsync")]
        public async Task<IActionResult> GetPatientAsync(int patientId)
        {
            var result = await _patientService.GetPatientAsync(patientId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            ReadPatientDTO patientDTO = (ReadPatientDTO)result.Result;

            return Ok(patientDTO);
        }


        [HttpDelete]
        [Route("DeletePatientAsync/{patientId}")]
        public async Task<IActionResult> DeletePatientAsync(int patientId)
        {
            var result = await _patientService.DeletePatientAsync(patientId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        [HttpPut]
        [Route("UpdatePatientAsync/{patientId}")]
        public async Task<IActionResult> UpdatePatientAsync(int patientId, UpdatePatientViewModel updatedPatient)
        {
            UpdatePatientDTO dto = new UpdatePatientDTO()
            {
                Id = patientId,
                FirstName = updatedPatient.FirstName,
                LastName = updatedPatient.LastName,
                Pesel = updatedPatient.Pesel,
                Gender = updatedPatient.Gender,
                DateOfBirth = updatedPatient.DateOfBirth,
                PhoneNumber = updatedPatient.PhoneNumber,
                Email = updatedPatient.Email,
                Region = updatedPatient.Region,
                City = updatedPatient.City,
                PostalCode = updatedPatient.PostalCode,
                Street = updatedPatient.Street,
                UnitNumber = updatedPatient.UnitNumber
            };

            var result = await _patientService.UpdatePatientAsync(dto);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }
        #endregion

    }
}
