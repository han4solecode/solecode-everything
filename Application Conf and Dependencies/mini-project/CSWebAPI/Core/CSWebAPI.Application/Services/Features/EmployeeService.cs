using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services.Interfaces;
using CSWebAPI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Application.Services.Features
{
    public class EmployeeService : IEmployeeService
    {
        private readonly CompanyOptions _options;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IOptions<CompanyOptions> options, IEmployeeRepository employeeRepository)
        {
            _options = options.Value;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> AddNewEmployee(Employee employee)
        {
            var itEmpCount = await _employeeRepository.ITEmpCount();
            var itDeptNo = await _employeeRepository.ITDeptNo();

            if (employee.Deptno == null)
            {
                await _employeeRepository.AddEmployee(employee);
                return true;
            }
            else if (employee.Deptno == itDeptNo && itEmpCount < _options.ITDeptMaxEmp)
            {
                await _employeeRepository.AddEmployee(employee);
                return true;
            }
            else if (employee.Deptno != itDeptNo)
            {
                await _employeeRepository.AddEmployee(employee);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return false;
            }

            try
            {
                await _employeeRepository.DeleteEmployee(employee);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees(int a, int b)
        {
            var employees = await _employeeRepository.GetAllEmployee(a, b);

            return employees;
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);

            return employee;
        }

        public async Task<bool> UpdateExistingEmployee(int id, Employee inputEmployee)
        {
            var emp = await _employeeRepository.GetEmployeeById(id);

            if (emp == null)
            {
                return false;
            }

            emp.Fname = inputEmployee.Fname;
            emp.Lname = inputEmployee.Lname;
            emp.Address = inputEmployee.Address;
            emp.Dob = inputEmployee.Dob;
            emp.Sex = inputEmployee.Sex;
            emp.Position = inputEmployee.Position;
            emp.Deptno = inputEmployee.Deptno;

            var itEmpCount = await _employeeRepository.ITEmpCount();
            var itDeptNo = await _employeeRepository.ITDeptNo();

            if (emp.Deptno == null)
            {
                // await _context.Employees.AddAsync(employee);
                // await _context.SaveChangesAsync();
                await _employeeRepository.UpdateEmployee(emp);
                return true;
            }
            else if (emp.Deptno == itDeptNo && itEmpCount < _options.ITDeptMaxEmp)
            {
                // await _context.Employees.AddAsync(employee);
                // await _context.SaveChangesAsync();
                await _employeeRepository.UpdateEmployee(emp);
                return true;
            }
            else if (emp.Deptno != itDeptNo)
            {
                // await _context.Employees.AddAsync(employee);
                // await _context.SaveChangesAsync();
                await _employeeRepository.UpdateEmployee(emp);
                return true;
            }

            return false;
        }
    }
}