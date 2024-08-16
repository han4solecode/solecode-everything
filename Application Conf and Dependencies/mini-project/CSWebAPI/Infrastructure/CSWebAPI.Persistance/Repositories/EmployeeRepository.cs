using CSWebAPI.Application.Repositories;
using CSWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Persistance.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployee(int recordsPerPage, int currentPage)
        {
            var employees = await _context.Employees.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
            return employees;
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee;
        }

        public async Task UpdateEmployee(Employee inputEmployee)
        {
            _context.Employees.Update(inputEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task<int> ITEmpCount()
        {
            var count = await _context.Employees.Include(e => e.DeptnoNavigation).Where(e => e.DeptnoNavigation.Deptname == "IT").CountAsync();

            return count;
        }

        public async Task<int> ITDeptNo()
        {
            var deptNo = await _context.Departments.Where(d => d.Deptname == "IT").Select(d => d.Deptno).SingleOrDefaultAsync();

            return deptNo;
        }

        public async Task<IEnumerable<Employee>> Born8090()
        {
            var employees = await _context.Employees.Where(e => e.Dob >= new DateOnly(1980, 1, 1) && e.Dob <= new DateOnly(1990, 1, 1)).ToListAsync();

            return employees;
        }

        public async Task<IEnumerable<Employee>> DueRetireManager(int retirementAge)
        {
            var managers = await _context.Employees.Where(e => e.Empno == e.Department.Mgrempno && (DateOnly.FromDateTime(DateTime.Now).Year - e.Dob.Year) == retirementAge).OrderBy(e => e.Lname).ToListAsync();

            return managers;
        }

        public async Task<IEnumerable<Employee>> EmpNotManager()
        {
            var employee = await _context.Employees.Where(e => e.Empno != e.Department.Mgrempno).ToListAsync();

            return employee;
        }

        public async Task<IEnumerable<Employee>> FemaleBornAfter90()
        {
            var employees = await _context.Employees.Where(e => e.Sex == "Female" && e.Dob >= new DateOnly(1990, 1, 1)).ToListAsync();

            return employees;
        }

        public async Task<IEnumerable<Employee>> FemaleManager()
        {
            var employees = await _context.Employees.Where(e => e.Sex == "Female" && e.Empno == e.Department.Mgrempno).OrderBy(e => e.Lname).ThenBy(e => e.Fname).ToListAsync();

            return employees;
        }

        public async Task<int> FemaleManagerCount()
        {
            var count = await _context.Employees.Where(e => e.Sex == "Female" && e.Empno == e.Department.Mgrempno).CountAsync();

            return count;
        }

        public async Task<IEnumerable<Employee>> FromBRICS()
        {
            var employees = await _context.Employees.Where(e => e.Address.Contains("Brazil") || e.Address.Contains("Russia") || e.Address.Contains("India") || e.Address.Contains("China") || e.Address.Contains("South Africa")).OrderBy(e => e.Lname).ToListAsync();

            return employees;
        }
    }
}