using MedicalResearchCenter.Data.DTOs.LabTest;
using MedicalResearchCenter.Services;
using MedicalResearchCenter.ViewModels.LabTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalResearchCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : ControllerBase
    {
        #region Properties

        private readonly LabTestService _labTestService;

        #endregion

        #region Constructors

        public LabTestController(LabTestService labTestService)
        {
            _labTestService = labTestService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("AddLabTestAsync")]
        public async Task<IActionResult> AddLabTestAsync(NewLabTestViewModel newLabTest)
        {
            CreateLabTestDTO dto = new CreateLabTestDTO()
            {
                Name = newLabTest.Name,
                Unit = newLabTest.Unit,
                NormFrom = newLabTest.NormFrom,
                NormTo = newLabTest.NormTo
            };

            var result = await _labTestService.AddLabTestAsync(dto);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            ReadLabTestDTO labTest = (ReadLabTestDTO)result.Result;

            return CreatedAtRoute("GetLabTestAsync", new { labTestId = labTest.Id }, labTest);
        }

        [HttpGet]
        [Route("GetLabTestAsync/{labTestId}", Name = "GetLabTestAsync")]
        public async Task<IActionResult> GetLabTestAsync(int labTestId)
        {
            var result = await _labTestService.GetLabTestAsync(labTestId);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            ReadLabTestDTO labTest = (ReadLabTestDTO)result.Result;

            return Ok(labTest);
        }

        [HttpPost]
        [Route("GetLabTestsAsync")]
        public async Task<IActionResult> GetLabTestsAsync(GetLabTestsViewModel paging)
        {
            GetLabTestsDTO dto = new GetLabTestsDTO()
            {
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber
            };

            var result = await _labTestService.GetLabTestsAsync(dto);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            GetLabTestsResponseDTO labTests = (GetLabTestsResponseDTO)result.Result;

            return Ok(labTests);
        }

        [HttpPut]
        [Route("UpdateLabTestAsync/{labTestId}")]
        public async Task<IActionResult> UpdateLabTestAsync(int labTestId, UpdateLabTestViewModel updatedLabTest)
        {
            UpdateLabTestDTO dto = new UpdateLabTestDTO()
            {
                Id = labTestId,
                Name = updatedLabTest.Name,
                Unit = updatedLabTest.Unit,
                NormFrom = updatedLabTest.NormFrom,
                NormTo = updatedLabTest.NormTo
            };

            var result = await _labTestService.UpdateLabTestAsync(dto);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        [HttpDelete]
        [Route("DeleteLabTestAsync/{labTestId}")]
        public async Task<IActionResult> DeleteLabTestAsync(int labTestId)
        {
            var result = await _labTestService.DeleteLabTestAsync(labTestId);

            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return StatusCode(result.StatusCode);
        }

        #endregion

    }
}
