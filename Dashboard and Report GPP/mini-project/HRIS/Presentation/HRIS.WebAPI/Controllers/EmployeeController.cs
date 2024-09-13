using System.Security.Claims;
using HRIS.Application.Contracts;
using HRIS.Application.DTOs.LeaveRequest;
using HRIS.Application.DTOs.Register;
using HRIS.Application.DTOs.Request;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuthService _authService;

        public EmployeeController(IEmployeeService employeeService, IAuthService authService)
        {
            _employeeService = employeeService;
            _authService = authService;
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateEmployeeAdmin([FromBody] RegisterRequestDto registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _authService.RegisterEmployeeAsync(registerRequest, "Administrator");

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Administrator, HR Manager")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployee([FromBody] RegisterRequestDto registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _authService.RegisterEmployeeAsync(registerRequest, "Employee");

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Administrator, HR Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var emps = await _employeeService.GetAllEmployees();

            return Ok(emps);
        }

        [Authorize(Roles = "Administrator, HR Manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var emp = await _employeeService.GetEmployeeById(id);

            return Ok(emp);
        }

        [Authorize(Roles = "Department Manager, Employee Supervisor, Employee")]
        [HttpGet("all")]
        public async Task<IActionResult> GetEmployeeFilterByRole()
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var detail = await _employeeService.GetEmployeeFilterByRole();

            return Ok(detail);
        }

        [Authorize(Roles = "Administrator, HR Manager, Employee")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] Employee inputEmployee)
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _employeeService.UpdateExistingEmployee(id, inputEmployee);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Administrator, HR Manager")]
        [HttpDelete("deactivate/{id}")]
        public async Task<IActionResult> DeactivateEmployee(string id, [FromBody] string reason)
        {
            var res = await _employeeService.DeactivateEmployee(id, reason);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("request/leave")]
        public async Task<IActionResult> EmployeeLeaveRequest([FromBody] LeaveRequestDto leaveRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _employeeService.EmployeeLeaveRequest(leaveRequest);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Employee Supervisor, HR Manager")]
        [HttpPost("request/review")]
        public async Task<IActionResult> ReviewRequest([FromBody] ReviewRequestDto reviewRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _employeeService.ReviewRequest(reviewRequest);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }
    }
}