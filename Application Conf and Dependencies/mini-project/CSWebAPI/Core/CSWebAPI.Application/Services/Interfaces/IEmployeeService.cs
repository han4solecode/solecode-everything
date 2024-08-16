using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> AddNewEmployee(Employee employee);

        Task<bool> UpdateExistingEmployee(int id, Employee inputEmployee);

        Task<IEnumerable<Employee>> GetAllEmployees(int a, int b);

        Task<Employee?> GetEmployeeById(int id);

        Task<bool> DeleteEmployee(int id);
    }
}