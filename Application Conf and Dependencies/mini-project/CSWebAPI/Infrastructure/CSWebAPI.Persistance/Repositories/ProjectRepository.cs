using CSWebAPI.Application.Repositories;
using CSWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Persistance.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        private readonly CompanyOptions _options;

        public ProjectRepository(AppDbContext appDbContext, IOptions<CompanyOptions> options)
        {
            _context = appDbContext;
            _options = options.Value;
        }

        public async Task AddProject(Project project)
        {
            var deptProjCount = await _context.Projects.Where(p => p.Deptno == project.Deptno).CountAsync();

            // check if dept project is less than 10
            if (deptProjCount < _options.MaxDeptProject)
            {
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();
            }
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

        public async Task<Project?> UpdateProject(int id, Project inputProject)
        {
            var projectToBeUpdated = await _context.Projects.FindAsync(id);

            if (projectToBeUpdated == null)
            {
                return null;
            }

            projectToBeUpdated.Projname = inputProject.Projname;
            projectToBeUpdated.Deptno = inputProject.Deptno;

            var deptProjCount = await _context.Projects.Where(p => p.Deptno == projectToBeUpdated.Deptno).CountAsync();

            // check if dept project is less than 10
            if (deptProjCount < _options.MaxDeptProject)
            {
                _context.Projects.Update(projectToBeUpdated);
                await _context.SaveChangesAsync();
                return projectToBeUpdated;
            } 
            else
            {
                return null;
            }
        }
    }
}