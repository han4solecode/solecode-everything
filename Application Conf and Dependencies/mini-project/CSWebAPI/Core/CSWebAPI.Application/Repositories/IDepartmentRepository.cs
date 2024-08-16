using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartment(int recordsPerPage, int currentPage);

        Task<Department?> GetDepartmentById(int id);

        Task AddDepartment(Department department);

        Task UpdateDepartment(Department inputDepartment);

        Task DeleteDepartment(Department department);
    }
}