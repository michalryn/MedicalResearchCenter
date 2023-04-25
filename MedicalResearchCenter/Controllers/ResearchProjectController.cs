using MedicalResearchCenter.Data.DTOs.Patient;
using MedicalResearchCenter.Data.DTOs.ResearchProject;
using MedicalResearchCenter.Services;
using MedicalResearchCenter.ViewModels.Patient;
using MedicalResearchCenter.ViewModels.ResearchProject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalResearchCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchProjectController : ControllerBase
    {
        #region Properties

        private readonly ResearchProjectService _researchProjectService;

        #endregion

        #region Constructors

        public ResearchProjectController(ResearchProjectService researchProjectService)
        {
            _researchProjectService = researchProjectService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronous method for creating a research project
        /// </summary>
        /// <param name="newProject">Object containing information about the research project</param>
        /// <returns>Object containing information about a new research project along with a route to the get method</returns>
        /// <response code="201">Object containing information about a new research project along with a route to the get method</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("AddResearchProjectAsync")]
        [ProducesResponseType(typeof(ReadRPDTO), 201)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> AddResearchProjectAsync(NewRPViewModel newProject)
        {
            CreateRPDTO dto = new CreateRPDTO()
            {
                Title = newProject.Title,
                Description = newProject.Description,
                Manager = newProject.Manager,
                StartDate = newProject.StartDate
            };

            var result = await _researchProjectService.AddResearchProjectAsync(dto);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            ReadRPDTO projectDTO = (ReadRPDTO)result.Result;

            return CreatedAtRoute("GetResearchProjectAsync", new { projectId = projectDTO.Id }, projectDTO);
        }

        /// <summary>
        /// Asynchronous method for loading a research project specified by an id
        /// </summary>
        /// <param name="projectId">Id of the research project</param>
        /// <returns>Object containing information about the research project</returns>
        /// <response code="200">Object containing information about the research project</response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpGet]
        [Route("GetResearchProjectAsync/{projectId}", Name = "GetResearchProjectAsync")]
        [ProducesResponseType(typeof(ReadRPDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetResearchProjectAsync(int projectId)
        {
            var result = await _researchProjectService.GetResearchProjectAsync(projectId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            ReadRPDTO projectDTO = (ReadRPDTO)result.Result;

            return Ok(projectDTO);
        }

        /// <summary>
        /// Asynchronous method for loading all research projects
        /// </summary>
        /// <remarks>
        /// The number of the first page is 1. Both "PageNumber" and "PageSize" have to be greater or equal to 1.
        /// </remarks>
        /// <param name="paging">Object containing information about paging</param>
        /// <returns>Object containing a list of research projects along with information about paging</returns>
        /// <response code="200">Object containing a list of research projects along with information about paging</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("GetResearchProjectsAsync")]
        [ProducesResponseType(typeof(ReadRPDTO), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetResearchProjectsAsync(GetRPsViewModel paging)
        {
            GetRPsDTO dto = new GetRPsDTO()
            {
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            var result = await _researchProjectService.GetResearchProjectsAsync(dto);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            GetRPsResponseDTO projects = (GetRPsResponseDTO)result.Result;

            return Ok(projects);
        }

        /// <summary>
        /// Asynchronous method for deletin a research project
        /// </summary>
        /// <remarks>
        /// Deleting a research project also removes all lab tests and referrals affiliated with the research project
        /// </remarks>
        /// <param name="projectId">Id of the research project</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpDelete]
        [Route("DeleteResearchProjectAsync/{projectId}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> DeleteResearchProjectAsync(int projectId)
        {
            var result = await _researchProjectService.DeleteResearchProjectAsync(projectId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        /// <summary>
        /// Asynchronous method for updating an existing research project
        /// </summary>
        /// <param name="projectId">Id of the research project</param>
        /// <param name="updatedProject">Object containing new information about the research project</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpPut]
        [Route("UpdateResearchProjectAsync/{projectId}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> UpdateResearchProjectAsync(int projectId, UpdateRPViewModel updatedProject)
        {
            UpdateRPDTO dto = new UpdateRPDTO()
            {
                Id = projectId,
                Title = updatedProject.Title,
                Description = updatedProject.Description,
                Manager = updatedProject.Manager,
                StartDate = updatedProject.StartDate,
                EndDate = updatedProject.EndDate
            };

            var result = await _researchProjectService.UpdateResearchProjectAsync(dto);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);

        }

        /// <summary>
        /// Asynchronous method for assigning a patient to a research project
        /// </summary>
        /// <param name="projectId">Id of the research project</param>
        /// <param name="patientId">Id of the patient</param>
        /// <returns>Success message if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="200">Success message</response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("AssignPatientAsync/{projectId}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> AssignPatientAsync(int projectId, int patientId)
        {
            var result = await _researchProjectService.AssignPatientAsync(projectId, patientId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        /// <summary>
        /// Asynchronous method for removing a patient from a research project
        /// </summary>
        /// <param name="projectId">Id of the research project</param>
        /// <param name="patientId">Id of the patient</param>
        /// <returns>Success message if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="200">Success message</response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("RemovePatientAsync/{projectId}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> RemovePatientAsync(int projectId, int patientId)
        {
            var result = await _researchProjectService.RemovePatientAsync(projectId, patientId);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        #endregion
    }
}
