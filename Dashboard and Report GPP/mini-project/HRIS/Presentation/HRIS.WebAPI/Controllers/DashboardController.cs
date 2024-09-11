using HRIS.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public DashboardController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
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
    }
}