using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Services.Interfaces
{
    public interface IInfoService
    {
        // a
        Task<IEnumerable<Employee>> FromBRICS();

        // b
        Task<IEnumerable<Employee>> Born8090();

        // c
        Task<IEnumerable<Employee>> FemaleBornAfter90();

        // d
        Task<IEnumerable<Employee>> FemaleManager();

        // e
        Task<IEnumerable<Employee>> EmpNotManager();

        // f
        Task<IEnumerable<Project>> PlanningProjects();

        // g
        Task<IEnumerable<Project>> NoEmpProject();

        // h
        Task<IEnumerable<Object>> ListDeptWithMore10Emp();

        // i
        Task<IEnumerable<Object>> ITDeptEmployees();

        // j
        Task<IEnumerable<Employee>> DueRetireManager();

        // k
        Task<IEnumerable<Object>> FemEmpHoursWorked();

        // l
        Task<int> FemaleManagerCount();

        // m
        Task<IEnumerable<Project>> ITAndHRProjects();

        // n
        Task<IEnumerable<Object>> EmpNotManagerAndSupervisor();

        // o
        Task<IEnumerable<Object>> FemaleManagerProjects();

        // p
        Task<Object> MaxAndMinWorkHours();

        // q
        Task<IEnumerable<Object>> EmpTotalWorkHours();

        // r
        Task<IEnumerable<Object>> EmpAge();

        // s
        Task<IEnumerable<Object>> EmpProjInfo();

        // t
        Task<IEnumerable<Object>> ManagerUnder40();
    }
}