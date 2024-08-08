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
    }
}