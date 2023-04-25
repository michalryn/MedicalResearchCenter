using MedicalResearchCenter.Data.DTOs.LabTest;
using MedicalResearchCenter.Data.DTOs.Patient;
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

        /// <summary>
        /// Asynchronous method for creating a lab test
        /// </summary>
        /// <param name="newLabTest">Object containing information about the lab test</param>
        /// <returns>Object containing information about a new lab test along with a route to the get method</returns>
        /// <response code="201">Object containing information about a new lab test along with a route to the get method</response>
        /// <response code="409">Error message</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [HttpPost]
        [Route("AddLabTestAsync")]
        [ProducesResponseType(typeof(ReadLabTestDTO), 201)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
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

        /// <summary>
        /// Asynchronous method for loading a lab test specified by an id
        /// </summary>
        /// <param name="labTestId">Lab test id</param>
        /// <returns>Object containing information about the lab test</returns>
        /// <response code="200">Object containing information about the lab test</response>
        /// <response code="404">Error message</response>
        /// <response code="500">Error message</response>
        [HttpGet]
        [Route("GetLabTestAsync/{labTestId}", Name = "GetLabTestAsync")]
        [ProducesResponseType(typeof(ReadLabTestDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
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

        /// <summary>
        /// Asynchronous method for loading all lab tests
        /// </summary>
        /// <remarks>
        /// The number of the first page is 1. Both "PageNumber" and "PageSize" have to be greater or equal to 1.
        /// </remarks>
        /// <param name="paging">Object containing information about paging</param>
        /// <returns>Object containing a list of lab tests along with information about paging</returns>
        /// <response code="200">Object containing a list of lab tests along with information about paging</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("GetLabTestsAsync")]
        [ProducesResponseType(typeof(GetLabTestsResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 500)]
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

        /// <summary>
        /// Asynchronous method for updating an existing lab test
        /// </summary>
        /// <param name="labTestId">Id of the lab test</param>
        /// <param name="updatedLabTest">Object containing new information about the lab test</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="409">Error message</response>
        /// <response code="500">Error message</response>
        [HttpPut]
        [Route("UpdateLabTestAsync/{labTestId}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
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

        /// <summary>
        /// Asynchronous method for deleting a lab test
        /// </summary>
        /// <param name="labTestId">Id of the lab test</param>
        /// <returns>Nothing if the method executes correctly and an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="404">Error message</response>
        /// <response code="409">Error message</response>
        /// <response code="500">Error message</response>
        [HttpDelete]
        [Route("DeleteLabTestAsync/{labTestId}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
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
