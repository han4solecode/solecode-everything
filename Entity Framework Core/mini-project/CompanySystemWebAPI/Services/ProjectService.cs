using CompanySystemWebAPI.Data;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanySystemWebAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Project>> GetAllProject(int recordsPerPage, int currentPage)
        {
            var projects = await _context.Projects.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
            return projects;
        }

        public async Task<Project?> GetProjectById(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            return project;
        }

        public async Task AddProject(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task<Project?> UpdateProject(int id, Project inputProject)
        {
            var projectToBeUpdated = await _context.Projects.FindAsync(id);

            if (projectToBeUpdated == null)
            {
                return null;
            }

            projectToBeUpdated.Projname = inputProject.Projname;
            projectToBeUpdated.Deptno = inputProject.Deptno;

            _context.Projects.Update(projectToBeUpdated);
            await _context.SaveChangesAsync();
            return projectToBeUpdated;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var projectToBeDeleted = await _context.Projects.FindAsync(id);

            if (projectToBeDeleted != null)
            {
                _context.Projects.Remove(projectToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Project>> NoEmpProject()
        {
            var workson = await _context.Worksons.Select(w => w.Projno).ToListAsync();
            var NoEmpProject = await _context.Projects.Where(p => !workson.Contains(p.Projno)).ToListAsync();

            return NoEmpProject;
        }

        public async Task<IEnumerable<Project>> ITAndHRProjects()
        {
            var projects = await _context.Projects.Where(p => p.DeptnoNavigation.Deptname == "IT" || p.DeptnoNavigation.Deptname == "HR").ToListAsync();

            return projects;
        }
    }
}