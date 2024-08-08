using CompanySystemWebAPI.Models;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployee(int recordsPerPage, int currentPage);

        Task<Employee?> GetEmployeeById(int id);

        Task AddEmployee(Employee employee);

        Task<Employee?> UpdateEmployee(int id, Employee inputEmployee);

        Task<bool> DeleteEmployee(int id);

        Task<IEnumerable<Employee>> FromBRICS();

        Task<IEnumerable<Employee>> Born8090();

        Task<IEnumerable<Employee>> FemaleBornAfter90();

        Task<IEnumerable<Employee>> FemaleManager();

        Task<IEnumerable<Object>> ITDeptEmployees();

        Task<IEnumerable<Employee>> DueRetireManager();

        Task<int> FemaleManagerCount();

        Task<IEnumerable<Employee>> ManagerUnder40();
    }
}