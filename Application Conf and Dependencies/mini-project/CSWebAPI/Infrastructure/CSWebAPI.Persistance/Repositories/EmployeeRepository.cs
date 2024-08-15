using CSWebAPI.Application.Repositories;
using CSWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Persistance.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly CompanyOptions _options;

        public EmployeeRepository(AppDbContext appDbContext, IOptions<CompanyOptions> options)
        {
            _context = appDbContext;
            _options = options.Value;
        }

        public async Task AddEmployee(Employee employee)
        {
            var itEmpCount = await _context.Employees.Include(e => e.DeptnoNavigation).Where(e => e.DeptnoNavigation.Deptname == "IT").CountAsync();

            // check if new employee dept is IT and IT emp count less than 9
            // if (employee.Department.Deptname == "IT")
            // {
            //     if (itEmpCount < _options.ITDeptMaxEmp)
            //     {

            //     }
            //     else
            //     {

            //     }
            // }

            // check if new employee dept is IT and IT emp count less than 9
            if (employee.Department.Deptname == "IT" && itEmpCount < _options.ITDeptMaxEmp)
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
            }
            else if (employee.Department.Deptname != "IT")
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
            }
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

        public async Task<Employee?> UpdateEmployee(int id, Employee inputEmployee)
        {
            var employeeToBeUpdated = await _context.Employees.FindAsync(id);
            var itEmpCount = await _context.Employees.Include(e => e.DeptnoNavigation).Where(e => e.DeptnoNavigation.Deptname == "IT").CountAsync();

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

            // check if the updated employee dept is IT and IT emp count less than 9
            if (employeeToBeUpdated.Department.Deptname == "IT" && itEmpCount < _options.ITDeptMaxEmp)
            {
                _context.Employees.Update(employeeToBeUpdated);
                await _context.SaveChangesAsync();
                return employeeToBeUpdated;
            }
            else if (employeeToBeUpdated.Department.Deptname != "IT")
            {
                _context.Employees.Update(employeeToBeUpdated);
                await _context.SaveChangesAsync();
                return employeeToBeUpdated;
            }

            return null;
        }
    }
}