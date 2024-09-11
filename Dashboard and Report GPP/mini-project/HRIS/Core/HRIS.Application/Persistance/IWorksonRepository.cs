using HRIS.Domain.Entity;

namespace HRIS.Application.Persistance
{
    public interface IWorksonRepository
    {
        Task<IEnumerable<Workson>> GetAll(int recordsPerPage, int currentPage);

        Task<Workson?> GetById(string empNo, int projNo);

        Task Create(Workson workson);

        Task Update(Workson workson);

        Task Delete(Workson workson);

        Task<IEnumerable<Workson>> GetAllNoPaging();

    }
}