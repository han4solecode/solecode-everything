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

        [HttpGet]
        public async Task<IActionResult> GetAllWorkson([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var worksons = await _worksonService.GetAllWorkson(recordsPerPage, currentPage);

            return Ok(worksons);
        }

        [HttpGet("{empNo}/{projNo}")]
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

        [HttpPost]
        public async Task<IActionResult> AddWorkson([FromBody] Workson workson)
        {
            await _worksonService.AddWorkson(workson);

            return Created($"api/v1/project/{workson.Empno}", workson);
        }

        [HttpPut("{empNo}/{projNo}")]
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

        [HttpDelete("{empNo}/{projNo}")]
        public async Task<IActionResult> DeleteWorkson(int empNo, int projNo)
        {
            if (empNo <= 0 || projNo <= 0)
            {
                return BadRequest();
            }

            var isWoDeleted = await _worksonService.DeleteWorkson(empNo, projNo);

            if (!isWoDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}