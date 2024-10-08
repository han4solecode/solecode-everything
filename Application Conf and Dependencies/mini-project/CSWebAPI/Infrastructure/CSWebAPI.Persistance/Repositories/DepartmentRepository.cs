using CSWebAPI.Application.Repositories;
using CSWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSWebAPI.Persistance.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartment(Department department)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartment(int recordsPerPage, int currentPage)
        {
            var departments = await _context.Departments.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
            return departments;
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            return department;
        }

        public async Task UpdateDepartment(Department inputDepartment)
        {
            _context.Departments.Update(inputDepartment);
            await _context.SaveChangesAsync();
        }
    }
}