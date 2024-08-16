using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProject(int recordsPerPage, int currentPage);

        Task<Project?> GetProjectById(int id);

        Task AddProject(Project project);

        Task UpdateProject(Project inputProject);

        Task DeleteProject(Project project);

        Task<int> DepartmentProjectCount(int projDeptNo);

        Task<IEnumerable<Project>> ITAndHRProjects();

        Task<IEnumerable<Project>> NoEmpProject();

        Task<IEnumerable<Project>> PlanningProjects();
    }
}