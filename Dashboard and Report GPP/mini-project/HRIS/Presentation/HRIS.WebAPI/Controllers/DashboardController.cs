using HRIS.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWorkflowService _workflowService;
        private readonly IProjectService _projectService;

        public DashboardController(IEmployeeService employeeService, IWorkflowService workflowService, IProjectService projectService)
        {
            _employeeService = employeeService;
            _workflowService = workflowService;
            _projectService = projectService;
        }

        [HttpGet("kpi")]
        public async Task<IActionResult> Kpi()
        {
            var employeeDistributionPerDepartment = await _employeeService.GetEmployeeDistributionPerDepartment();
            var top5Employees = await _employeeService.GetTop5BestEmployee();
            var avgSalaryPerDept = await _employeeService.GetAverageSalaryPerDepartment();

            var data = new {
                EmployeeDistribution = employeeDistributionPerDepartment,
                TopEmployees = top5Employees,
                AverageSalaryPerDepartment = avgSalaryPerDept
            };

            return Ok(data);
        }

        [Authorize(Roles = "Employee Supervisor, HR Manager")]
        [HttpGet("processes")]
        public async Task<IActionResult> Processes()
        {
            var processToReview = await _workflowService.GetProcessToReview();

            return Ok(processToReview);
        }

        [HttpGet("leaves/total-per-type")]
        public async Task<IActionResult> TotalLeavesPerType([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            var res = await _workflowService.GetTotalLeavesTakenPerLeaveType(startDate, endDate);

            return Ok(res);
        }

        [HttpGet("leaves/total-per-type/report")]
        public async Task<IActionResult> GenerateTotalLeavesPerTypeReport([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            var fileName = "LeaveReport.pdf";
            var file = await _workflowService.GenerateTotalLeavesTakenPerLeaveTypeReport(startDate, endDate);

            return File(file, "application/pdf", fileName);
        }

        [HttpGet("project")]
        public async Task<IActionResult> GetProjectReport()
        {
            var res = await _projectService.GetProjectReport();

            return Ok(res);
        }

        [HttpGet("project/report")]
        public async Task<IActionResult> GenerateProjectReport()
        {
            var fileName = "ProjetReport.pdf";
            var file = await _projectService.GenerateProjectReport();

            return File(file, "application/pdf", fileName);
        }

        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployeeByDepartment([FromQuery] int page, [FromQuery] string department)
        {
            if (page <= 0)
            {
                return BadRequest();
            }

            var employees = await _employeeService.GetEmployeeFilterByDepartment(page, department);

            return Ok(employees);
        }

        [HttpGet("employee/report")]
        public async Task<IActionResult> GenerateEmployeeByDeprtmentReport([FromQuery] string department)
        {
            var fileName = $"{department}_Employee_Report.pdf";

            var file = await _employeeService.GenerateEmployeeFilterByDepartmentReport(department);

            return File(file, "application/pdf", fileName);
        }
    }
}