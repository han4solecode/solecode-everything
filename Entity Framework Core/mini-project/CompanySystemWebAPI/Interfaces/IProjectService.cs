using CompanySystemWebAPI.Models;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProject(int recordsPerPage, int currentPage);

        Task<Project?> GetProjectById(int id);

        Task AddProject(Project project);

        Task<Project?> UpdateProject(int id, Project inputProject);

        Task<bool> DeleteProject(int id);
    }
}