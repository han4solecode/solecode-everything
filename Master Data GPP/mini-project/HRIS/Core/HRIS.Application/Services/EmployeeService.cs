using HRIS.Application.Contracts;
using HRIS.Application.Persistance;
using HRIS.Application.Persistance.Helper;
using HRIS.Domain.Entity;

namespace HRIS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> AddNewEmployee(Employee employee)
        {
            try
            {
                await _employeeRepository.Create(employee);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteExistingEmployee(int id)
        {
            var employee = await _employeeRepository.GetById(id);

            if (employee == null)
            {
                return false;
            }

            try
            {
                await _employeeRepository.Delete(employee);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees(int a, int b)
        {
            var employees = await _employeeRepository.GetAll(a, b);

            return employees;
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetById(id);

            return employee;
        }

        public async Task<IEnumerable<object>> SearchEmployee(EmployeeQueryObject query, EmployeeSortObject sort, int a, int b)
        {
            var employees = await _employeeRepository.SearchEmployee(query, sort, a, b);

            return employees;
        }

        public async Task<IEnumerable<object>> GetEmployeeDetail(int id)
        {
            var employee = await _employeeRepository.GetDetail(id);

            return employee;
        }

        public async Task<bool> UpdateExistingEmployee(int id, Employee inputEmployee)
        {
            var employee = await _employeeRepository.GetById(id);

            if (employee == null)
            {
                return false;
            }

            employee.Address = inputEmployee.Address;
            employee.Deptno = inputEmployee.Deptno;
            employee.Dob = inputEmployee.Dob;
            employee.Email = inputEmployee.Email;
            employee.Empdependents = inputEmployee.Empdependents;
            employee.Employmenttype = inputEmployee.Employmenttype;
            employee.Fname = inputEmployee.Fname;
            employee.Level = inputEmployee.Level;
            employee.Lname = inputEmployee.Lname;
            employee.Phonenumber = inputEmployee.Phonenumber;
            employee.Position = inputEmployee.Position;
            employee.Salary = inputEmployee.Salary;
            employee.Sex = inputEmployee.Sex;
            employee.Ssn = inputEmployee.Ssn;
            // employee.Status = inputEmployee.Status;

            await _employeeRepository.Update(employee);

            return true;
        }

        public async Task<bool> DeactivateEmployee(int id, string? reason)
        {
            var emp = await _employeeRepository.GetById(id);

            if (emp == null)
            {
                return false;
            }

            try
            {
                await _employeeRepository.DeactivateEmployee(emp, reason);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}