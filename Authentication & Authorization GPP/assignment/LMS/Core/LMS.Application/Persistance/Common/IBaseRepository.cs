using LMS.Domain.Common;

namespace LMS.Application.Persistance.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<T?> GetById(int id);

        Task<IEnumerable<T>> GetAll(int recordsPerPage, int currentPage);
    }
}