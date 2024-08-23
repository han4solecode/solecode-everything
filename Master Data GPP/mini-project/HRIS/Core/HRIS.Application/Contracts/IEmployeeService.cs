using HRIS.Application.Persistance.Helper;
using HRIS.Domain.Entity;

namespace HRIS.Application.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees(int a, int b);

        Task<Employee?> GetEmployeeById(int id);

        Task<bool> AddNewEmployee(Employee employee);

        Task<bool> UpdateExistingEmployee(int id, Employee inputEmployee);

        Task<bool> DeleteExistingEmployee(int id);

        Task<IEnumerable<object>> SearchEmployee(EmployeeQueryObject query, EmployeeSortObject sort, int a, int b);

        Task<IEnumerable<object>> GetEmployeeDetail(int id);

        Task<bool> DeactivateEmployee(int id, string? reason);
    }
}