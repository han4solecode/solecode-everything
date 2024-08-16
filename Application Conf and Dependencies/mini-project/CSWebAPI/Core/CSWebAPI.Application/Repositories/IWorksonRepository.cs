using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Repositories
{
    public interface IWorksonRepository
    {
        Task<IEnumerable<Workson>> GetAllWorkson(int recordsPerPage, int currentPage);

        Task<Workson?> GetWorksonById(int empNo, int projNo);

        Task AddWorkson(Workson workson);

        Task UpdateWorkson(Workson inputWorkson);

        Task DeleteWorkson(Workson workson);

        Task<int> TotalHoursWorkedInProject(int projNo);

        Task<int> AssignedEmployeeCount(int empNo);
    }
}