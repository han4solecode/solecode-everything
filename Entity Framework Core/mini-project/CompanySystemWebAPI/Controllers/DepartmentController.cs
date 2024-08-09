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

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllDepartment([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var departments = await _departmentService.GetAllDepartment(recordsPerPage, currentPage);

            return Ok(departments);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
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

        [HttpPost]
        [MapToApiVersion("1.0")]
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

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
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

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
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