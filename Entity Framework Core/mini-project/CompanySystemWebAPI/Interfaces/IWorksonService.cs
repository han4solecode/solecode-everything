using CompanySystemWebAPI.Models;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IWorksonService
    {
        Task<IEnumerable<Workson>> GetAllWorkson(int recordsPerPage, int currentPage);

        Task<Workson?> GetWorksonById(int empNo, int projNo);

        Task AddWorkson(Workson workson);

        Task<Workson?> UpdateWorkson(int empNo, int projNo, Workson inputWorkson);

        Task<bool> DeleteWorkson(int empNo, int projNo);
    }
}