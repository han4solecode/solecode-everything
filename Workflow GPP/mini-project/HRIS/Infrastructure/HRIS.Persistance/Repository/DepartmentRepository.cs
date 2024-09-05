using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Persistance.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(Department entity)
        {
            await _context.Departments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Department entity)
        {
            _context.Departments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAll(int recordsPerPage, int currentPage)
        {
            var departments = await _context.Departments.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();

            return departments;
        }

        public async Task<Department?> GetById(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            return department;
        }

        public async Task Update(Department entity)
        {
            _context.Departments.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}