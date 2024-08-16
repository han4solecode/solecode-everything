using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Services.Interfaces
{
    public interface IWorksonService
    {
        Task<IEnumerable<Workson>> GetAllWorksons(int a, int b);

        Task<Workson?> GetWorksonById(int empNo, int projNo);

        Task<bool> AddNewWorkson(Workson workson);

        Task<bool> UpdateExistingWorkson(int empNo, int projNo, Workson inputWorkson);

        Task<bool> DeleteWorkson(int empNo, int projNo);
    }
}