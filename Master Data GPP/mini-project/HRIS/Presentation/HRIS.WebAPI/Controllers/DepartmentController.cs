using HRIS.Application.Contracts;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
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
        public async Task<IActionResult> GetAllDepartments([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var departments = await _departmentService.GetAllDepartments(recordsPerPage, currentPage);

            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            var isCreated = await _departmentService.AddNewDepartment(department);

            if (!isCreated)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Deptno}, department);
        }
    }
}