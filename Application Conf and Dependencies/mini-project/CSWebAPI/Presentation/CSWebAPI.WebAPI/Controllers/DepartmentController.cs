using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services.Interfaces;
using CSWebAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CSWebAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartment([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var departments = await _departmentService.GetAllDepartments(recordsPerPage, currentPage);

            return Ok(departments);
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            try
            {
                await _departmentService.AddNewDepartment(department);

                return Created($"api/v1/department/{department.Deptno}", department);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department inputDepartment)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var deptUpdated = await _departmentService.UpdateExistingDepartment(id, inputDepartment);

                if (!deptUpdated)
                {
                    return NotFound();
                }

                return Ok(inputDepartment);
            }
            catch (System.Exception)
            {   
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
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
    }
}