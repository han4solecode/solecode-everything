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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Retrive all department data with pagination
        /// </summary>
        /// <param name="recordsPerPage"></param>
        /// <param name="currentPage"></param>
        /// <returns>A list of departments</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/department?recordsPerPage=3&amp;currentPage=1
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of departments</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDepartment([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var departments = await _departmentService.GetAllDepartment(recordsPerPage, currentPage);

            return Ok(departments);
        }

        /// <summary>
        /// Retrive a department data from its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A department data</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/department/2
        /// 
        /// </remarks>
        /// <response code="200">Returns a department data</response>
        /// <response code="400">If id is invalid</response>
        /// <response code="404">If the department does not exist</response>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var department = await _departmentService.GetDepartmentById(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        /// <summary>
        /// Create a new department
        /// </summary>
        /// <param name="department"></param>
        /// <returns>A newly created department</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/v1/department
        ///     {
        ///         "deptname": "Markeing",
        ///         "mgremno": "7
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created department data</response>
        /// <response code="400">If POST request is unsuccessful</response>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            try
            {
                await _departmentService.AddDepartment(department);

                return Created($"api/v1/department/{department.Deptno}", department);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update an existing department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDepartment"></param>
        /// <returns>A newly updated department</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/v1/department/4
        ///     {
        ///         "deptname": "RnD",
        ///         "mgrempno": 13 
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly updated department data</response>
        /// <response code="400">If PUT request is unsuccessful</response>
        /// <response code="404">If the department does not exist</response>
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department inputDepartment)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var deptUpdated = await _departmentService.UpdateDepartment(id, inputDepartment);

                if (deptUpdated == null)
                {
                    return NotFound();
                }

                return Ok(deptUpdated);
            }
            catch (System.Exception)
            {   
                return BadRequest();
            }

        }

        /// <summary>
        /// Delete an existing department
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/v1/department/4
        /// 
        /// </remarks>
        /// <response code="204">Delete request is successful</response>
        /// <response code="400">Delete request is successful</response>
        /// <response code="404">If the department does not exist</response>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isDeptDeleted = await _departmentService.DeleteDepartment(id);

                if (!isDeptDeleted)
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

        [HttpGet]
        [Route("emp-count")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> ListDeptWithMore10Emp()
        {
            var departments = await _departmentService.ListDeptWithMore10Emp();

            return Ok(departments);
        }
    }
}