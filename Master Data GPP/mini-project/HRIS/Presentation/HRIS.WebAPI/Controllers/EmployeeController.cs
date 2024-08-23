using HRIS.Application.Contracts;
using HRIS.Application.Persistance.Helper;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var employees = await _employeeService.GetAllEmployees(recordsPerPage, currentPage);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);

            return Ok(employee);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployee([FromQuery] int recordsPerPage, [FromQuery] int currentPage, [FromQuery] EmployeeQueryObject query, [FromQuery] EmployeeSortObject sort)
        {
            var employees = await _employeeService.SearchEmployee(query, sort, recordsPerPage, currentPage);

            return Ok(employees);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetEmployeeDetail(int id)
        {
            var employee = await _employeeService.GetEmployeeDetail(id);

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var isCreated = await _employeeService.AddNewEmployee(employee);

            if (!isCreated)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Empno}, employee);
        }

        [HttpPatch("{id}/deactive")]
        public async Task<IActionResult> DeactivateEmployee(int id, [FromBody] string? reason)
        {
            var isDeactivated = await _employeeService.DeactivateEmployee(id, reason);

            if (!isDeactivated)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}