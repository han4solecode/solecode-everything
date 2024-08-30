using HRIS.Application.Persistance.Common;
using HRIS.Domain.Entity;

namespace HRIS.Application.Persistance
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllNoPaging();
    }
}