using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Persistance.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(Project entity)
        {
            await _context.Projects.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Project entity)
        {
            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> GetAll(int recordsPerPage, int currentPage)
        {
            var projects = await _context.Projects.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();

            return projects;
        }

        public async Task<IEnumerable<Project>> GetAllNoPaging()
        {
            var projects = await _context.Projects.ToListAsync();

            return projects;
        }

        public async Task<Project?> GetById(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            return project;
        }

        public async Task Update(Project entity)
        {
            _context.Projects.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}