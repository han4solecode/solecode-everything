using CompanySystemWebAPI.Models;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartment(int recordsPerPage, int currentPage);

        Task<IEnumerable<Department>> GetDepartmentById(int id);

        Task<Department> AddDepartment(Department department);

        Task<Employee> UpdateDepartment(int id, Department inputDepartment);

        Task<bool> DeleteEmployee(int id);
    }
}