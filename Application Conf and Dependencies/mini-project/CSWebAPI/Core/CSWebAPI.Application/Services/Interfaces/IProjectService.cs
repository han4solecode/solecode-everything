using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjects(int a, int b);

        Task<Project?> GetProjectById(int id);

        Task<bool> AddNewProject(Project project);

        Task<bool> UpdateExistingProject(int id, Project inputProject);

        Task<bool> DeleteProject(int id);
    }
}