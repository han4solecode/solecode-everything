using CompanySystemWebAPI.Data;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanySystemWebAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext appDbContext)
        {
            _context = appDbContext;
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

        public async Task AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task<Department?> UpdateDepartment(int id, Department inputDepartment)
        {
            var departmentToBeUpdated = await _context.Departments.FindAsync(id);

            if (departmentToBeUpdated == null)
            {
                return null;
            }

            departmentToBeUpdated.Deptname = inputDepartment.Deptname;
            departmentToBeUpdated.Mgrempno = inputDepartment.Mgrempno;

            _context.Departments.Update(departmentToBeUpdated);
            await _context.SaveChangesAsync();
            return departmentToBeUpdated;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var departmentToBeDeleted = await _context.Departments.FindAsync(id);

            if (departmentToBeDeleted != null)
            {
                _context.Departments.Remove(departmentToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}