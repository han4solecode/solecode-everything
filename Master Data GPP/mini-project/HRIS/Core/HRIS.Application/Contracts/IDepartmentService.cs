using HRIS.Domain.Entity;

namespace HRIS.Application.Contracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartments(int a, int b);

        Task<Department?> GetDepartmentById(int id);

        Task<bool> AddNewDepartment(Department department);

        Task<bool> UpdateExistingDepartment(int id, Department inputDepartment);

        Task<bool> DeleteExistingDepartment(int id);
    }
}