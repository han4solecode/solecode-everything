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
            // var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Empno == id);
            var employee = await _context.Employees.FindAsync(id);
            return employee;
        }

        public async Task AddEmployee(Employee employee)
        {
            // var newEmployee = new Employee()
            // {
            //     Fname = employee.Fname,
            // };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            // return employee;
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
    }
}