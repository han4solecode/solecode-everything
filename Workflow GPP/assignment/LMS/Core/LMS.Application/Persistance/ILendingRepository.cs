using LMS.Application.Persistance.Common;
using LMS.Domain.Entities;

namespace LMS.Application.Persistance
{
    public interface ILendingRepository : IBaseRepository<Lending>
    {
        Task<object> GetUserLendingInfo(string id);
    }
}