using CompanySystemWebAPI.Models;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartment(int recordsPerPage, int currentPage);

        Task<Department?> GetDepartmentById(int id);

        Task AddDepartment(Department department);

        Task<Department?> UpdateDepartment(int id, Department inputDepartment);

        Task<bool> DeleteDepartment(int id);
    }
}