using MedicalResearchCenter.Data.DTOs.ResearchProject;
using MedicalResearchCenter.Services;
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

        [HttpPost]
        [Route("AddResearchProjectAsync")]
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

        [HttpGet]
        [Route("GetResearchProjectAsync/{projectId}", Name = "GetResearchProjectAsync")]
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

        [HttpDelete]
        [Route("DeleteResearchProjectAsync/{projectId}")]
        public async Task<IActionResult> DeleteResearchProjectAsync(int projectId)
        {
            var result = await _researchProjectService.DeleteResearchProjectAsync(projectId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        [HttpPut]
        [Route("UpdateResearchProjectAsync/{projectId}")]
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
        #endregion
    }
}
