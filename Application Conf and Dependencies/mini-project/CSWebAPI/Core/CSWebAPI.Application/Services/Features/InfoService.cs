using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services.Interfaces;
using CSWebAPI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Application.Services.Features
{
    public class InfoService : IInfoService
    {
        private readonly IInfoRepository _infoRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly CompanyOptions _options;

        public InfoService(IInfoRepository infoRepository, IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IOptions<CompanyOptions> options)
        {
            _infoRepository = infoRepository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _options = options.Value;
        }

        public async Task<IEnumerable<Employee>> Born8090()
        {
            var val = await _employeeRepository.Born8090();

            return val;
        }

        public async Task<IEnumerable<Employee>> DueRetireManager()
        {
            var val = await _employeeRepository.DueRetireManager(_options.RetirementAge);

            return val;
        }

        public async Task<IEnumerable<object>> EmpAge()
        {
            var val = await _infoRepository.EmpAge();

            return val;
        }

        public async Task<IEnumerable<Employee>> EmpNotManager()
        {
            var val = await _employeeRepository.EmpNotManager();

            return val;
        }

        public async Task<IEnumerable<object>> EmpNotManagerAndSupervisor()
        {
            var val = await _infoRepository.EmpNotManagerAndSupervisor();

            return val;
        }

        public async Task<IEnumerable<object>> EmpProjInfo()
        {
            var val = await _infoRepository.EmpProjInfo();

            return val;
        }

        public async Task<IEnumerable<object>> EmpTotalWorkHours()
        {
            var val = await _infoRepository.EmpTotalWorkHours();

            return val;
        }

        public async Task<IEnumerable<Employee>> FemaleBornAfter90()
        {
            var val = await _employeeRepository.FemaleBornAfter90();

            return val;
        }

        public async Task<IEnumerable<Employee>> FemaleManager()
        {
            var val = await _employeeRepository.FemaleManager();

            return val;
        }

        public async Task<int> FemaleManagerCount()
        {
            var val = await _employeeRepository.FemaleManagerCount();

            return val;
        }

        public async Task<IEnumerable<object>> FemaleManagerProjects()
        {
            var val = await _infoRepository.FemaleManagerProjects();

            return val;
        }

        public async Task<IEnumerable<object>> FemEmpHoursWorked()
        {
            var val = await _infoRepository.FemEmpHoursWorked();

            return val;
        }

        public async Task<IEnumerable<Employee>> FromBRICS()
        {
            var val = await _employeeRepository.FromBRICS();

            return val;
        }

        public async Task<IEnumerable<Project>> ITAndHRProjects()
        {
            var val = await _projectRepository.ITAndHRProjects();

            return val;
        }

        public async Task<IEnumerable<object>> ITDeptEmployees()
        {
            var val = await _infoRepository.ITDeptEmployees();

            return val;
        }

        public async Task<IEnumerable<object>> ListDeptWithMore10Emp()
        {
            var val = await _infoRepository.ListDeptWithMore10Emp();

            return val;
        }

        public async Task<IEnumerable<object>> ManagerUnder40()
        {
            var val = await _infoRepository.ManagerUnder40();

            return val;
        }

        public async Task<object> MaxAndMinWorkHours()
        {
            var val = await _infoRepository.MaxAndMinWorkHours();

            return val;
        }

        public async Task<IEnumerable<Project>> NoEmpProject()
        {
            var val = await _projectRepository.NoEmpProject();

            return val;
        }

        public async Task<IEnumerable<Project>> PlanningProjects()
        {
            var val = await _projectRepository.PlanningProjects();

            return val;
        }
    }
}