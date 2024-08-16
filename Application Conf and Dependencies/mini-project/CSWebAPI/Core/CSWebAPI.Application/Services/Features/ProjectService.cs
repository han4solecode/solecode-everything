using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services.Interfaces;
using CSWebAPI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Application.Services.Features
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly CompanyOptions _options;

        public ProjectService(IProjectRepository projectRepository, IOptions<CompanyOptions> options)
        {
            _projectRepository = projectRepository;
            _options = options.Value;
        }

        public async Task<bool> AddNewProject(Project project)
        {
            var deptProjCount = await _projectRepository.DepartmentProjectCount(project.Deptno);

            // check if dept project is less than 10
            if (deptProjCount < _options.MaxDeptProject)
            {
                await _projectRepository.AddProject(project);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var project = await _projectRepository.GetProjectById(id);

            if (project == null)
            {
                return false;
            }

            try
            {
                await _projectRepository.DeleteProject(project);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
            
        }

        public async Task<IEnumerable<Project>> GetAllProjects(int a, int b)
        {
            var projects = await _projectRepository.GetAllProject(a, b);

            return projects;
        }

        public async Task<Project?> GetProjectById(int id)
        {
            var project = await _projectRepository.GetProjectById(id);

            return project;
        }

        public async Task<bool> UpdateExistingProject(int id, Project inputProject)
        {
            var proj = await _projectRepository.GetProjectById(id);

            if (proj == null)
            {
                return false;
            }

            try
            {
                proj.Projname = inputProject.Projname;
                proj.Deptno = inputProject.Deptno;

                var count = await _projectRepository.DepartmentProjectCount(proj.Deptno);

                if (count < _options.MaxDeptProject)
                {
                    await _projectRepository.UpdateProject(proj);
                    return true;
                }

                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}