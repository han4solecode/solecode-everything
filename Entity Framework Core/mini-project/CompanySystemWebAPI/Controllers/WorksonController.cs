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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorksonById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var workson = await _worksonService.GetWorksonById(id);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkson(int id, [FromBody] Workson inputWorkson)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var woUpdated = await _worksonService.UpdateWorkson(id, inputWorkson);

            if (woUpdated == null)
            {
                return NotFound();
            }

            return Ok(woUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkson(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var isWoDeleted = await _worksonService.DeleteWorkson(id);

            if (!isWoDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}