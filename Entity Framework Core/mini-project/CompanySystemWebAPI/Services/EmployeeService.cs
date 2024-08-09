using CompanySystemWebAPI.Data;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanySystemWebAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext appDbContext)
        {
            _context = appDbContext;
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

        public async Task AddEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee?> UpdateEmployee(int id, Employee inputEmployee)
        {
            var employeeToBeUpdated = await _context.Employees.FindAsync(id);

            if (employeeToBeUpdated == null)
            {
                return null;
            }

            employeeToBeUpdated.Fname = inputEmployee.Fname;
            employeeToBeUpdated.Lname = inputEmployee.Lname;
            employeeToBeUpdated.Address = inputEmployee.Address;
            employeeToBeUpdated.Dob = inputEmployee.Dob;
            employeeToBeUpdated.Sex = inputEmployee.Sex;
            employeeToBeUpdated.Position = inputEmployee.Position;
            employeeToBeUpdated.Deptno = inputEmployee.Deptno;
            
            _context.Employees.Update(employeeToBeUpdated);
            await _context.SaveChangesAsync();
            return employeeToBeUpdated;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employeeToBeDeleted = await _context.Employees.FindAsync(id);

            if (employeeToBeDeleted != null)
            {
                _context.Employees.Remove(employeeToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Employee>> FromBRICS()
        {
            var employees = await _context.Employees.Where(e => e.Address.Contains("Brazil") || e.Address.Contains("Russia") || e.Address.Contains("India") || e.Address.Contains("China") || e.Address.Contains("South Africa")).OrderBy(e => e.Lname).ToListAsync();

            return employees;
        }

        public async Task<IEnumerable<Employee>> Born8090()
        {
            var employees = await _context.Employees.Where(e => e.Dob >= new DateOnly(1980, 1, 1) && e.Dob <= new DateOnly(1990, 1, 1)).ToListAsync();

            return employees;
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

        public async Task<IEnumerable<Object>> ITDeptEmployees()
        {
            var employees = await _context.Employees.Include(e => e.DeptnoNavigation).Where(e => e.DeptnoNavigation.Deptname == "IT").Select(e => new {
                Name = $"{e.Fname} {e.Lname}",
                Address = e.Address
            }).ToListAsync();

            return employees;
        }

        public async Task<IEnumerable<Employee>> DueRetireManager()
        {
            var managers = await _context.Employees.Where(e => e.Empno == e.Department.Mgrempno && (DateOnly.FromDateTime(DateTime.Now).Year - e.Dob.Year) == 40).OrderBy(e => e.Lname).ToListAsync();

            return managers;
        }

        public async Task<int> FemaleManagerCount()
        {
            var count = await _context.Employees.Where(e => e.Sex == "Female" && e.Empno == e.Department.Mgrempno).CountAsync();

            return count;
        }

        public async Task<IEnumerable<Object>> ManagerUnder40()
        {
            var managers = await _context.Employees.Where(e => e.Empno == e.Department.Mgrempno && (DateOnly.FromDateTime(DateTime.Now).Year - e.Dob.Year) < 40).Select(m => new {
                Name = $"{m.Fname} {m.Lname}",
                Age = DateOnly.FromDateTime(DateTime.Now).Year - m.Dob.Year
            }).ToListAsync();

            return managers;
        }

    }
}