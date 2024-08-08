using CompanySystemWebAPI.Models;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IWorksonService
    {
        Task<IEnumerable<Workson>> GetAllWorkson(int recordsPerPage, int currentPage);

        Task<Workson?> GetWorksonById(int id);

        Task AddWorkson(Workson workson);

        Task<Workson?> UpdateWorkson(int id, Workson inputWorkson);

        Task<bool> DeleteWorkson(int id);
    }
}