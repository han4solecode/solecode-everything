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
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllEmployee([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var employees = await _employeeService.GetAllEmployee(recordsPerPage, currentPage);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _employeeService.AddEmployee(employee);

            return Created($"api/v1/employee/{employee.Empno}", employee);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
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

        [HttpGet]
        [Route("from-brics")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> FromBRICS()
        {
            var employees = await _employeeService.FromBRICS();

            return Ok(employees);
        }

        [HttpGet]
        [Route("born-between-8090")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> Born8090()
        {
            var employees = await _employeeService.Born8090();

            return Ok(employees);
        }

        [HttpGet]
        [Route("female-90")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> FemaleBornAfter90()
        {
            var employees = await _employeeService.FemaleBornAfter90();

            return Ok(employees);
        }

        [HttpGet]
        [Route("female-manager")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> FemaleManager()
        {
            var employees = await _employeeService.FemaleManager();

            return Ok(employees);
        }

        [HttpGet]
        [Route("it-employee")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> ITDeptEmployees()
        {
            var employees = await _employeeService.ITDeptEmployees();

            return Ok(employees);
        }

        [HttpGet]
        [Route("retire-manager")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> DueRetireManager()
        {
            var managers = await _employeeService.DueRetireManager();

            return Ok(managers);
        }

        [HttpGet]
        [Route("female-manager-count")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> FemaleManagerCount()
        {
            var count = await _employeeService.FemaleManagerCount();

            return Ok(count);
        }

        [HttpGet]
        [Route("manager-under40")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> ManagerUnder40()
        {
            var managers = await _employeeService.ManagerUnder40();

            return Ok(managers);
        }
    }
}