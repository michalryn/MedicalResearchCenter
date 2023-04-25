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

        /// <summary>
        /// Asynchronous method for creating a patient
        /// </summary>
        /// <param name="newPatient">Object containing information about the patient</param>
        /// <returns>Object containing information about a new patient along with a route to the get method</returns>
        /// <response code="201">Object containing information about a new patient along with a route to the get method</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("AddPatientAsync")]
        [ProducesResponseType(typeof(ReadPatientDTO), 201)]
        [ProducesResponseType(typeof(string), 500)]
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

        /// <summary>
        /// Asynchronous method for loading a patient specified by an id
        /// </summary>
        /// <param name="patientId">Id of the patient</param>
        /// <returns>Object containing information about the patient</returns>
        /// <response code="200">Object containing information about the patient</response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpGet]
        [Route("GetPatientAsync/{patientId}", Name = "GetPatientAsync")]
        [ProducesResponseType(typeof(ReadPatientDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
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

        /// <summary>
        /// Asynchronous method for deleting a patient
        /// </summary>
        /// <remarks>
        /// Deleting a patient also removes all lab tests and referrals affiliated with the patient
        /// </remarks>
        /// <param name="patientId">Id of the patient</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpDelete]
        [Route("DeletePatientAsync/{patientId}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> DeletePatientAsync(int patientId)
        {
            var result = await _patientService.DeletePatientAsync(patientId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        /// <summary>
        /// Asynchronous method for updating an existing patient
        /// </summary>
        /// <param name="patientId">Id of the patient</param>
        /// <param name="updatedPatient">Object containing new information about the patient</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpPut]
        [Route("UpdatePatientAsync/{patientId}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
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

        /// <summary>
        /// Asynchronous method for loading all patients
        /// </summary>
        /// <remarks>
        /// The number of the first page is 1. Both "PageNumber" and "PageSize" have to be greater or equal to 1.
        /// </remarks>
        /// <param name="paging">Object containing information about paging</param>
        /// <returns>Object containing a list of patients along with information about paging</returns>
        /// <response code="200">Object containing a list of patients along with information about paging</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("GetPatientsAsync")]
        [ProducesResponseType(typeof(GetPatientsResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetPatientsAsync(GetPatientsViewModel paging)
        {
            GetPatientsDTO dto = new GetPatientsDTO()
            {
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            var result = await _patientService.GetPatientsAsync(dto);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            GetPatientsResponseDTO patients = (GetPatientsResponseDTO)result.Result;

            return Ok(patients);
        }

        /// <summary>
        /// Asynchronous method for loading all patients assigned to the research project specified by an id
        /// </summary>
        /// <remarks>
        /// The number of the first page is 1. Both "PageNumber" and "PageSize" have to be greater or equal to 1.
        /// </remarks>
        /// <param name="projectId">Id of the research project</param>
        /// <param name="paging">Object containing information about paging</param>
        /// <returns>Object containing a list of patients along with information about paging</returns>
        /// <response code="200">Object containing a list of patients along with information about paging</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("GetAssignedPatients/{projectId}")]
        [ProducesResponseType(typeof(GetPatientsResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetAssignedPatientsAsync(int projectId, GetPatientsViewModel paging)
        {
            GetPatientsDTO dto = new GetPatientsDTO()
            {
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize,
                ProjectId = projectId,
                Assigned = true
            };

            var result = await _patientService.GetPatientsAsync(dto);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            GetPatientsResponseDTO patients = (GetPatientsResponseDTO)result.Result;

            return Ok(patients);
        }

        /// <summary>
        /// Asynchronous method for loading all patients not assigned to the research project specified by an id
        /// </summary>
        /// <remarks>
        /// The number of the first page is 1. Both "PageNumber" and "PageSize" have to be greater or equal to 1.
        /// </remarks>
        /// <param name="projectId">Id of the research project</param>
        /// <param name="paging">Object containing information about paging</param>
        /// <returns>Object containing a list of patients along with information about paging</returns>
        /// <response code="200">Object containing a list of patients along with information about paging</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("GetNotAssignedPatients/{projectId}")]
        [ProducesResponseType(typeof(GetPatientsResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetNotAssignedPatientsAsync(int projectId, GetPatientsViewModel paging)
        {
            GetPatientsDTO dto = new GetPatientsDTO()
            {
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize,
                ProjectId = projectId,
                Assigned = false
            };

            var result = await _patientService.GetPatientsAsync(dto);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            GetPatientsResponseDTO patients = (GetPatientsResponseDTO)result.Result;

            return Ok(patients);
        }
        #endregion

    }
}
