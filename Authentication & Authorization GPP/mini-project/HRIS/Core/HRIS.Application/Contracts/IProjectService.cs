using HRIS.Application.DTOs;
using HRIS.Domain.Entity;

namespace HRIS.Application.Contracts
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjects(int a, int b);

        Task<Project?> GetProjectById(int id);

        Task<bool> AddNewProject(Project project);

        Task<bool> UpdateExistingProject(int id, Project inputProject);

        Task<bool> DeleteProject(int id);

        // Task<object> GetProjectAssignment();

        Task<IEnumerable<Project>> GetAllProjectsByManager(string userId);
        Task<BaseResponseDto> AddNewProjectByManager(string userId, Project project);
        Task<BaseResponseDto> UpdateExistingProjectByManager(string userId, int projNo, Project inputProject);
        Task<BaseResponseDto> DeleteProjectByManager(string userId, int projNo);
    }
}