using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services.Interfaces;
using CSWebAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CSWebAPI.WebAPI.Controllers
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
        public async Task<IActionResult> GetAllEmployee([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var employees = await _employeeService.GetAllEmployees(recordsPerPage, currentPage);

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
            var isCreated = await _employeeService.AddNewEmployee(employee);

            if (isCreated)
            {
                return Created($"api/v1/employee/{employee.Empno}", employee);
            }
            else
            {
                return BadRequest("sumtingwong");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee inputEmployee)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {    
                var empUpdated = await _employeeService.UpdateExistingEmployee(id, inputEmployee);

                if (!empUpdated)
                {
                    return NotFound();
                }

                return Ok(inputEmployee);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isEmpDeleted = await _employeeService.DeleteEmployee(id);

                if (!isEmpDeleted)
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