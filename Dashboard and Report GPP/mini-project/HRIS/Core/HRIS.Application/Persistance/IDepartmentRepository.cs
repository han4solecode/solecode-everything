using HRIS.Application.Persistance.Common;
using HRIS.Domain.Entity;

namespace HRIS.Application.Persistance
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllNoPaging();
    }
}