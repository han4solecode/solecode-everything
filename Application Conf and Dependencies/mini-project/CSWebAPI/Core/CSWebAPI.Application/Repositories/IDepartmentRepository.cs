using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartment(int recordsPerPage, int currentPage);

        Task<Department?> GetDepartmentById(int id);

        Task AddDepartment(Department department);

        Task<Department?> UpdateDepartment(int id, Department inputDepartment);

        Task<bool> DeleteDepartment(int id);
    }
}