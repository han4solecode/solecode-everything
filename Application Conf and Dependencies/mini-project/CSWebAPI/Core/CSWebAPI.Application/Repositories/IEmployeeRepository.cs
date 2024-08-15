using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployee(int recordsPerPage, int currentPage);

        Task<Employee?> GetEmployeeById(int id);

        Task AddEmployee(Employee employee);

        Task<Employee?> UpdateEmployee(int id, Employee inputEmployee);

        Task<bool> DeleteEmployee(int id);
    }
}