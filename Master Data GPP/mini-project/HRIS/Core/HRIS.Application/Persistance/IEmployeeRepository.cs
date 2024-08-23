using HRIS.Application.Persistance.Common;
using HRIS.Application.Persistance.Helper;
using HRIS.Domain.Entity;

namespace HRIS.Application.Persistance
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<IEnumerable<Object>> SearchEmployee(EmployeeQueryObject query, EmployeeSortObject sort, int recordsPerPage, int currentPage);

        Task<IEnumerable<object>> GetDetail(int id);

        Task DeactivateEmployee(Employee employee, string? reason);
    }
}