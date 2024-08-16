using CSWebAPI.Application.Services;
using CSWebAPI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CSWebAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        // private readonly ICompanyService _companyService;
        private readonly IInfoService _infoService;

        public InfoController(IInfoService infoService)
        {
            // _companyService = companyService;
            _infoService = infoService;
        }

        [HttpGet("Born8090")]
        public async Task<IActionResult> Born8090()
        {
            var employees = await _infoService.Born8090();

            return Ok(employees);
        }

        [HttpGet("DueRetireManager")]
        public async Task<IActionResult> DueRetireManager()
        {
            var managers = await _infoService.DueRetireManager();

            return Ok(managers);
        }

        [HttpGet("EmpAge")]
        public async Task<IActionResult> EmpAge()
        {
            var employee = await _infoService.EmpAge();

            return Ok(employee);
        }

        [HttpGet("EmpNotManager")]
        public async Task<IActionResult> EmpNotManager()
        {
            var employee = await _infoService.EmpNotManager();

            return Ok(employee);
        }

        [HttpGet("EmpNotManagerAndSupervisor")]
        public async Task<IActionResult> EmpNotManagerAndSupervisor()
        {
            var employee = await _infoService.EmpNotManagerAndSupervisor();

            return Ok(employee);
        }

        [HttpGet("EmpProjInfo")]
        public async Task<IActionResult> EmpProjInfo()
        {
            var empProjInfo = await _infoService.EmpProjInfo();

            return Ok(empProjInfo);
        }

        [HttpGet("EmpTotalWorkHours")]
        public async Task<IActionResult> EmpTotalWorkHours()
        {
            var empTotalWorkHours = await _infoService.EmpTotalWorkHours();

            return Ok(empTotalWorkHours);
        }

        [HttpGet("FemaleBornAfter90")]
        public async Task<IActionResult> FemaleBornAfter90()
        {
            var employees = await _infoService.FemaleBornAfter90();

            return Ok(employees);
        }

        [HttpGet("FemaleManager")]
        public async Task<IActionResult> FemaleManager()
        {
            var femManager = await _infoService.FemaleManager();

            return Ok(femManager);
        }

        [HttpGet("FemaleManagerCount")]
        public async Task<IActionResult> FemaleManagerCount()
        {
            var count = await _infoService.FemaleManagerCount();

            return Ok(count);
        }

        [HttpGet("FemaleManagerProjects")]
        public async Task<IActionResult> FemaleManagerProjects()
        {
            var projects = await _infoService.FemaleManagerProjects();

            return Ok(projects);
        }

        [HttpGet("FemEmpHoursWorked")]
        public async Task<IActionResult> FemEmpHoursWorked()
        {
            var femEmp = await _infoService.FemEmpHoursWorked();

            return Ok(femEmp);
        }

        [HttpGet("FromBRICS")]
        public async Task<IActionResult> FromBRICS()
        {
            var employees = await _infoService.FromBRICS();

            return Ok(employees);
        }

        [HttpGet("ITAndHRProjects")]
        public async Task<IActionResult> ITAndHRProjects()
        {
            var projects = await _infoService.ITAndHRProjects();

            return Ok(projects);
        }

        [HttpGet("ITDeptEmployees")]
        public async Task<IActionResult> ITDeptEmployees()
        {
            var employees = await _infoService.ITDeptEmployees();

            return Ok(employees);
        }

        [HttpGet("ListDeptWithMore10Emp")]
        public async Task<IActionResult> ListDeptWithMore10Emp()
        {
            var depts = await _infoService.ListDeptWithMore10Emp();

            return Ok(depts);
        }

        [HttpGet("ManagerUnder40")]
        public async Task<IActionResult> ManagerUnder40()
        {
            var managers = await _infoService.ManagerUnder40();

            return Ok(managers);
        }

        [HttpGet("MaxAndMinWorkHours")]
        public async Task<IActionResult> MaxAndMinWorkHours()
        {
            var workHours = await _infoService.MaxAndMinWorkHours();

            return Ok(workHours);
        }

        [HttpGet("NoEmpProject")]
        public async Task<IActionResult> NoEmpProject()
        {
            var projects = await _infoService.NoEmpProject();

            return Ok(projects);
        }

        [HttpGet("PlanningProjects")]
        public async Task<IActionResult> PlanningProjects()
        {
            var projects = await _infoService.PlanningProjects();

            return Ok(projects);
        }
    }
}