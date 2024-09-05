using System.Security.Claims;
using HRIS.Application.Contracts;
using HRIS.Application.DTOs.Department;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllDepartment([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var departments = await _departmentService.GetAllDepartments(recordsPerPage, currentPage);

            return Ok(departments);
        }

        [Authorize(Roles = "Administrator")]
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

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdded = await _departmentService.AddNewDepartment(department);

            if (!isAdded)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Deptno }, department);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department inputDepartment)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _departmentService.UpdateExistingDepartment(id, inputDepartment);

            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok(inputDepartment);
        }

        [Authorize(Roles = "Administrator")]
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

        [Authorize(Roles = "Department Manager, Employee")]
        [HttpGet("info")]
        public async Task<IActionResult> GetDepartmentInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var deptInfo = await _departmentService.GetDepartmentInfo(userId!);

            return Ok(deptInfo);
        }

        [Authorize(Roles = "Department Manager")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDepartment([FromBody] Department inputDepartment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isUpdated = await _departmentService.UpdateDeptByManager(userId!, inputDepartment);

            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok(inputDepartment);
        }

        [Authorize(Roles = "HR Manager")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignDepartment([FromBody] DepartmentAssignRequestDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _departmentService.AssignDepartment(data.EmpNo, data.DeptNo);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }
        
    }
}