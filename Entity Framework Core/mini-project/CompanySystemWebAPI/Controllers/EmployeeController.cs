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

        /// <summary>
        /// Retrive all employee data with pagination
        /// </summary>
        /// <param name="recordsPerPage"></param>
        /// <param name="currentPage"></param>
        /// <returns>A list of employees</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/employee?recordsPerPage=3&amp;currentPage=1
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of employees</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployee([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var employees = await _employeeService.GetAllEmployee(recordsPerPage, currentPage);

            return Ok(employees);
        }

        /// <summary>
        /// Retrive an employee data from its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An employee data</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/employee/2
        /// 
        /// </remarks>
        /// <response code="200">Returns an employee data</response>
        /// <response code="400">If id is invalid</response>
        /// <response code="404">If the employee does not exist</response>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Create a new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>A newly created employee</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/v1/employee
        ///     {
        ///         "fname": "Bisma",
        ///         "lname": "Tungs",
        ///         "address": "Rock Street, Canada",
        ///         "dob": "1997-04-11",
        ///         "sex": "Female",
        ///         "position": "Staff of HR"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created employee data</response>
        /// <response code="400">If POST request is unsuccessful</response>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            try
            {
                await _employeeService.AddEmployee(employee);

                return Created($"api/v1/employee/{employee.Empno}", employee);
            }
            catch (System.Exception)
            {
                return BadRequest("Something is wrong");
            }

        }

        /// <summary>
        /// Update an existing employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputEmployee"></param>
        /// <returns>A newly updated employee</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/v1/employee/5
        ///     {
        ///         "fname": "Bisma",
        ///         "lname": "Tungs",
        ///         "address": "Mud Street, Canada",
        ///         "dob": "1997-04-11",
        ///         "sex": "Female",
        ///         "position": "Staff of HR",
        ///         "deptno": 2
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly updated employee data</response>
        /// <response code="400">If PUT request is unsuccessful</response>
        /// <response code="404">If the employee does not exist</response>
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee inputEmployee)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {    
                var empUpdated = await _employeeService.UpdateEmployee(id, inputEmployee);

                if (empUpdated == null)
                {
                    return NotFound();
                }

                return Ok(empUpdated);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Delete an existing employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/v1/employee/5
        /// 
        /// </remarks>
        /// <response code="204">Delete request is successful</response>
        /// <response code="400">Delete request is successful</response>
        /// <response code="404">If the employee does not exist</response>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Retrieve employees from BRICS (Brazil, Russia, India, China, South Africa) countries in alphabetical order of surname
        /// </summary>
        /// <returns>A list of employees</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v2/employee/from-brics
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of employees</response>
        [HttpGet]
        [Route("from-brics")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FromBRICS()
        {
            var employees = await _employeeService.FromBRICS();

            return Ok(employees);
        }

        /// <summary>
        /// Retrieve employees born between 1980–1990
        /// </summary>
        /// <returns>A list of employees</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v2/employee/born-between-8090
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of employees</response>
        [HttpGet]
        [Route("born-between-8090")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Born8090()
        {
            var employees = await _employeeService.Born8090();

            return Ok(employees);
        }

        /// <summary>
        /// Retrieve employees who are female and born after 1990
        /// </summary>
        /// <returns>A list of employees</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v2/employee/female-90
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of employees</response>
        [HttpGet]
        [Route("female-90")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FemaleBornAfter90()
        {
            var employees = await _employeeService.FemaleBornAfter90();

            return Ok(employees);
        }

        /// <summary>
        /// Retrieve managers who are female in alphabetical order of surname, and then first name
        /// </summary>
        /// <returns>A list of employees</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v2/employee/female-manager
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of employees</response>
        [HttpGet]
        [Route("female-manager")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FemaleManager()
        {
            var employees = await _employeeService.FemaleManager();

            return Ok(employees);
        }

        /// <summary>
        /// Retrieve a list of the names and addresses of all employees who work for the IT department
        /// </summary>
        /// <returns>A list of employee full name and address</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v2/employee/it-employee
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of employee full name and address</response>
        [HttpGet]
        [Route("it-employee")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ITDeptEmployees()
        {
            var employees = await _employeeService.ITDeptEmployees();

            return Ok(employees);
        }

        /// <summary>
        /// Retrieve a complete list of all managers who are due to retire this year, in alphabetical order of surname
        /// </summary>
        /// <returns>A list of employees</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v2/employee/retire-manager
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of employees</response>
        [HttpGet]
        [Route("retire-manager")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DueRetireManager()
        {
            var managers = await _employeeService.DueRetireManager();

            return Ok(managers);
        }

        /// <summary>
        /// Retrieve how many female managers
        /// </summary>
        /// <returns>Count of female managers</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v2/employee/female-manager-count
        /// 
        /// </remarks>
        /// <response code="200">Returns the count of female managers</response>
        [HttpGet]
        [Route("female-manager-count")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FemaleManagerCount()
        {
            var count = await _employeeService.FemaleManagerCount();

            return Ok(count);
        }

        /// <summary>
        /// Retrieve all managers under 40 years of age
        /// </summary>
        /// <returns>A list of employee fullname and age</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v2/employee/manager-under40
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of employee fullname and age</response>
        [HttpGet]
        [Route("manager-under40")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ManagerUnder40()
        {
            var managers = await _employeeService.ManagerUnder40();

            return Ok(managers);
        }
    }
}