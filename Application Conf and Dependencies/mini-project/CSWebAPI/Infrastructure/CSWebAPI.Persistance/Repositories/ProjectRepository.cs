using CSWebAPI.Application.Repositories;
using CSWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Persistance.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddProject(Project project)
        {

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
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

        public async Task UpdateProject(Project inputProject)
        {
            _context.Projects.Update(inputProject);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DepartmentProjectCount(int projDeptNo)
        {
            var deptProjCount = await _context.Projects.Where(p => p.Deptno == projDeptNo).CountAsync();

            return deptProjCount;
        }

        public async Task<IEnumerable<Project>> ITAndHRProjects()
        {
            var projects = await _context.Projects.Where(p => p.DeptnoNavigation.Deptname == "IT" || p.DeptnoNavigation.Deptname == "HR").ToListAsync();

            return projects;
        }

        public async Task<IEnumerable<Project>> NoEmpProject()
        {
            var workson = await _context.Worksons.Select(w => w.Projno).ToListAsync();
            var NoEmpProject = await _context.Projects.Where(p => !workson.Contains(p.Projno)).ToListAsync();

            return NoEmpProject;
        }

        public async Task<IEnumerable<Project>> PlanningProjects()
        {
            var projects = await _context.Projects.Where(p => p.DeptnoNavigation.Deptname == "Planning").ToListAsync();

            return projects;
        }
    }
}