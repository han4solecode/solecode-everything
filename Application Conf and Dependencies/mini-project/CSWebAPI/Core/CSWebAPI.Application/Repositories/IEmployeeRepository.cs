using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployee(int recordsPerPage, int currentPage);

        Task<Employee?> GetEmployeeById(int id);

        Task AddEmployee(Employee employee);

        Task UpdateEmployee(Employee inputEmployee);

        Task DeleteEmployee(Employee employee);

        Task<int> ITEmpCount();

        Task<int> ITDeptNo();

        Task<IEnumerable<Employee>> Born8090();

        Task<IEnumerable<Employee>> DueRetireManager(int retirementAge);

        Task<IEnumerable<Employee>> EmpNotManager();

        Task<IEnumerable<Employee>> FemaleBornAfter90();

        Task<IEnumerable<Employee>> FemaleManager();

        Task<int> FemaleManagerCount();

        Task<IEnumerable<Employee>> FromBRICS();
    }
}