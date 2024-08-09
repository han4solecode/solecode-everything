using Asp.Versioning;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystemWebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class WorksonController : ControllerBase
    {
        private readonly IWorksonService _worksonService;

        public WorksonController(IWorksonService worksonService)
        {
            _worksonService = worksonService;
        }

        /// <summary>
        /// Retrive all workson data with pagination
        /// </summary>
        /// <param name="recordsPerPage"></param>
        /// <param name="currentPage"></param>
        /// <returns>A list of worksons</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/workson?recordsPerPage=3&amp;currentPage=1
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of worksons</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWorkson([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var worksons = await _worksonService.GetAllWorkson(recordsPerPage, currentPage);

            return Ok(worksons);
        }

        /// <summary>
        /// Retrive a workson data from its id
        /// </summary>
        /// <param name="empNo"></param>
        /// <param name="projNo"></param>
        /// <returns>An workson data</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/workson/2/4
        /// 
        /// </remarks>
        /// <response code="200">Returns an workson data</response>
        /// <response code="400">If id is invalid</response>
        /// <response code="404">If the workson does not exist</response>
        [HttpGet("{empNo}/{projNo}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWorksonById(int empNo, int projNo)
        {
            if (empNo <= 0 || projNo <= 0)
            {
                return BadRequest();
            }

            var workson = await _worksonService.GetWorksonById(empNo, projNo);

            if (workson == null)
            {
                return NotFound();
            }

            return Ok(workson);
        }

        /// <summary>
        /// Create a new workson
        /// </summary>
        /// <param name="workson"></param>
        /// <returns>A newly created workson</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/v1/workson
        ///     {
        ///         "empNo": 7,
        ///         "projNo": 7,
        ///         "dateWorked": "2024-06-21",
        ///         "hoursWorked": 58
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created workson data</response>
        /// <response code="400">If POST request is unsuccessful</response>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddWorkson([FromBody] Workson workson)
        {
            try
            {
                await _worksonService.AddWorkson(workson);

                return Created($"api/v1/project/{workson.Empno}", workson);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update an existing workson
        /// </summary>
        /// <param name="empNo"></param>
        /// <param name="projNo"></param>
        /// <param name="inputWorkson"></param>
        /// <returns>A newly updated workson</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/v1/workson/2/4
        ///     {
        ///         "empNo": 4,
        ///         "projNo": 2,
        ///         "dateWorked": "2024-05-18",
        ///         "hoursWorked": 42
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly updated workson data</response>
        /// <response code="400">If PUT request is unsuccessful</response>
        /// <response code="404">If the workson does not exist</response>
        [HttpPut("{empNo}/{projNo}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateWorkson(int empNo, int projNo, [FromBody] Workson inputWorkson)
        {
            if (empNo <= 0 || projNo <= 0)
            {
                return BadRequest();
            }

            var woUpdated = await _worksonService.UpdateWorkson(empNo, projNo, inputWorkson);

            if (woUpdated == null)
            {
                return NotFound();
            }

            return Ok(woUpdated);
        }

        /// <summary>
        /// Delete an existing workson
        /// </summary>
        /// <param name="empNo"></param>
        /// <param name="projNo"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/v1/workson/4/2
        /// 
        /// </remarks>
        /// <response code="204">Delete request is successful</response>
        /// <response code="400">Delete request is successful</response>
        /// <response code="404">If the workson does not exist</response>
        [HttpDelete("{empNo}/{projNo}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DeleteWorkson(int empNo, int projNo)
        {
            if (empNo <= 0 || projNo <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isWoDeleted = await _worksonService.DeleteWorkson(empNo, projNo);

                if (!isWoDeleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }


        }

    }
}