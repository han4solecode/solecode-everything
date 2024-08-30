using HRIS.Application.Contracts;
using HRIS.Application.DTOs;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace HRIS.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<Employee> _userManager;

        public ProjectService(IProjectRepository projectRepository, UserManager<Employee> userManager)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
        }

        public async Task<bool> AddNewProject(Project project)
        {
            try
            {
                await _projectRepository.Create(project);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProject(int id)
        {
            var proj = await _projectRepository.GetById(id);

            if (proj == null)
            {
                return false;
            }

            await _projectRepository.Delete(proj);
            return true;
        }

        public async Task<IEnumerable<Project>> GetAllProjects(int a, int b)
        {
            var projs = await _projectRepository.GetAll(a, b);

            return projs;
        }

        public async Task<Project?> GetProjectById(int id)
        {
            var proj = await _projectRepository.GetById(id);

            return proj;
        }

        public async Task<bool> UpdateExistingProject(int id, Project inputProject)
        {
            var proj = await _projectRepository.GetById(id);

            if (proj == null)
            {
                return false;
            }

            proj.Projname = inputProject.Projname;
            proj.Deptno = inputProject.Deptno;

            await _projectRepository.Update(proj);

            return true;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsByManager(string userId)
        {
            var manager = await _userManager.FindByIdAsync(userId);

            var projects = await _projectRepository.GetAllNoPaging();

            var projectInDept = projects.Where(p => p.Deptno == manager!.Deptno);

            return projectInDept;
        }

        public async Task<BaseResponseDto> AddNewProjectByManager(string userId, Project project)
        {
            var manager = await _userManager.FindByIdAsync(userId);

            project.Deptno = manager!.Deptno;

            try
            {
                await _projectRepository.Create(project);
                return new BaseResponseDto 
                {
                    Status = "Success",
                    Message = "Project created successfully"
                };
            }
            catch (System.Exception)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Can't create new project"
                };
            }
        }

        public async Task<BaseResponseDto> UpdateExistingProjectByManager(string userId, int projNo, Project inputProject)
        {
            var manager = await _userManager.FindByIdAsync(userId);

            var projs = await _projectRepository.GetAllNoPaging();

            var projectInDept = projs.Where(p => p.Deptno == manager!.Deptno);

            var isAvailable = projectInDept.Any(p => p.Projno == projNo);

            if (!isAvailable)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Update project request denied. Please check your privilegdes."
                };
            }

            var projectToBeUpdated = await _projectRepository.GetById(projNo);

            projectToBeUpdated!.Projname = inputProject.Projname;

            await _projectRepository.Update(projectToBeUpdated);

            return new BaseResponseDto
            {
                Status = "Success",
                Message = "Project updated successfully"
            };
        }

        public async Task<BaseResponseDto> DeleteProjectByManager(string userId, int projNo)
        {
            var manager = await _userManager.FindByIdAsync(userId);

            var projs = await _projectRepository.GetAllNoPaging();

            var projectInDept = projs.Where(p => p.Deptno == manager!.Deptno);

            var isAvailable = projectInDept.Any(p => p.Projno == projNo);

            if (!isAvailable)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Delete project request denied. Please check your privilegdes."
                };
            }

            var projectToBeDeleted = await _projectRepository.GetById(projNo);

            await _projectRepository.Delete(projectToBeDeleted!);

            return new BaseResponseDto
            {
                Status = "Success",
                Message = "Project deleted successfully"
            };
        }
    }
}