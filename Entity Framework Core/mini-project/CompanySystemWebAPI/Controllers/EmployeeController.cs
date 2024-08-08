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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var employees = await _employeeService.GetAllEmployee(recordsPerPage, currentPage);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _employeeService.AddEmployee(employee);

            return Created($"api/v1/employee/{employee.Empno}", employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee inputEmployee)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var empUpdated = await _employeeService.UpdateEmployee(id, inputEmployee);

            if (empUpdated == null)
            {
                return NotFound();
            }

            return Ok(empUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var isEmpDeleted = await _employeeService.DeleteEmployee(id);

            if (!isEmpDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}