using HRIS.Application.Contracts;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorksonController : ControllerBase
    {
        private readonly IWorksonService _worksonService;

        public WorksonController(IWorksonService worksonService)
        {
            _worksonService = worksonService;
        }

        [Authorize(Roles = "Administrator, Employee Supervisor")]
        [HttpPost]
        public async Task<IActionResult> CreateWorkson([FromBody] Workson workson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _worksonService.AddNewWorkson(workson);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Administrator, Employee Supervisor")]
        [HttpDelete("{empNo}/{projNo}")]
        public async Task<IActionResult> DeleteWorkson(string empNo, int projNo)
        {
            if (projNo <= 0)
            {
                return BadRequest();
            }

            var res = await _worksonService.DeleteWorkson(empNo, projNo);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Administrator, Employee Supervisor, Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAllWorksons()
        {
            var res = await _worksonService.GetAllWorksons();

            return Ok(res);
        }

        [Authorize(Roles = "Administrator, Employee Supervisor")]
        [HttpGet("{empNo}/{projNo}")]
        public async Task<IActionResult> GetWorksonById(string empNo, int projNo)
        {
            if (projNo <= 0)
            {
                return BadRequest();
            }

            var res = await _worksonService.GetWorksonById(empNo, projNo);

            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }

        [Authorize(Roles = "Administrator, Employee Supervisor, Department Manager")]
        [HttpPut("{empNo}/{projNo}")]
        public async Task<IActionResult> UpdateWorkson(string empNo, int projNo, [FromBody] Workson inputWorkson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (projNo <= 0)
            {
                return BadRequest();
            }

            var res = await _worksonService.UpdateExistingWorkson(empNo, projNo, inputWorkson);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

    }
}