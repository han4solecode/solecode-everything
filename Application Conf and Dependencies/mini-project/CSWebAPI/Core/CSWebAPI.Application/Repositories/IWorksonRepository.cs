using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Repositories
{
    public interface IWorksonRepository
    {
        Task<IEnumerable<Workson>> GetAllWorkson(int recordsPerPage, int currentPage);

        Task<Workson?> GetWorksonById(int empNo, int projNo);

        Task AddWorkson(Workson workson);

        Task<Workson?> UpdateWorkson(int empNo, int projNo, Workson inputWorkson);

        Task<bool> DeleteWorkson(int empNo, int projNo);
    }
}