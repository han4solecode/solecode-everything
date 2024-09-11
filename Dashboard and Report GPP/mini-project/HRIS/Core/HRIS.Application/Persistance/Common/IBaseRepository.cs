namespace HRIS.Application.Persistance.Common
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll(int recordsPerPage, int currentPage);

        Task<T?> GetById(int id);

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);
    }
}