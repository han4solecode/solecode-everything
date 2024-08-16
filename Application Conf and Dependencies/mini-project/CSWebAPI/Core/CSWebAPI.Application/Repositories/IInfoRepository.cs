namespace CSWebAPI.Application.Repositories
{
    public interface IInfoRepository
    {
        Task<IEnumerable<object>> EmpAge();

        Task<IEnumerable<object>> EmpNotManagerAndSupervisor();

        Task<IEnumerable<object>> EmpProjInfo();

        Task<IEnumerable<object>> EmpTotalWorkHours();

        Task<IEnumerable<object>> FemaleManagerProjects();

        Task<IEnumerable<object>> FemEmpHoursWorked();

        Task<IEnumerable<object>> ITDeptEmployees();

        Task<IEnumerable<object>> ListDeptWithMore10Emp();

        Task<IEnumerable<object>> ManagerUnder40();

        Task<object> MaxAndMinWorkHours();
    }
}