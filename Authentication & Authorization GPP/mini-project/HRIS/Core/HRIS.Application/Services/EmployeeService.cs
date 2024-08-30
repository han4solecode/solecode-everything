using HRIS.Application.Contracts;
using HRIS.Application.DTOs;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponseDto> DeactivateEmployee(string id, string? reason)
        {
            var emp = await _userManager.FindByIdAsync(id);

            if (emp == null)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Employee does not exist"
                };
            }

            try
            {
                emp.Status = "Not Active";
                emp.DeactivatedAt = DateOnly.FromDateTime(DateTime.Now);
                emp.Deactreason = reason;

                await _userManager.UpdateAsync(emp);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Employee deactivated successfully"
                };
            }
            catch (System.Exception)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Employee cannot be deactivated"
                };
            }
        }

        public async Task<BaseResponseDto> DeleteExistingEmployee(string id)
        {
            var emp = await _userManager.FindByIdAsync(id);

            if (emp == null)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Employee does not exist"
                };
            }

            try
            {
                await _userManager.DeleteAsync(emp);
                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Employee deleted successfully"
                };
            }
            catch (System.Exception)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Employee cannot be deleted"
                };
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var emps = await _userManager.Users.Where(e => e.Status == "Active").ToListAsync();

            return emps;
        }

        public async Task<Employee?> GetEmployeeById(string id)
        {
            var emp = await _userManager.FindByIdAsync(id);

            return emp;
        }

        public async Task<IEnumerable<object>> GetEmployeeFilterByRole()
        {
            // get user form httpcontext
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var emp = await _userManager.FindByNameAsync(userName!);

            // var emp = await _userManager.FindByIdAsync(userId);

            var empRoles = await _userManager.GetRolesAsync(emp!);


            if (empRoles.Any(r => r == "Department Manager"))
            {
                var empInDept = await _userManager.Users.Where(e => e.Deptno == emp!.Deptno && e.Status == "Active").Select(e => new
                {
                    Name = $"{e.Fname} {e.Lname}",
                    Address = e.Address,
                    PhoneNumber = e.PhoneNumber,
                    Email = e.Email,
                    Supervisor = $"{e.SupervisorempnoNavigation!.Fname} {e.SupervisorempnoNavigation.Lname}",
                    EmploymentType = e.Employmenttype,
                    Level = e.Level,
                    e.Sex,
                    DOB = e.Dob,
                    e.Salary
                }).ToListAsync();

                return empInDept;
            }
            if (empRoles.Any(r => r == "Employee Supervisor"))
            {
                var supervisedEmp = await _userManager.Users.Where(e => e.Supervisorempno == emp!.Id && e.Status == "Active").Select(e => new
                    {
                        Name = $"{e.Fname} {e.Lname}",
                        Address = e.Address,
                        PhoneNumber = e.PhoneNumber,
                        Email = e.Email,
                        Supervisor = $"{e.SupervisorempnoNavigation!.Fname} {e.SupervisorempnoNavigation.Lname}",
                        EmploymentType = e.Employmenttype,
                        Level = e.Level,
                        e.Sex,
                        DOB = e.Dob,
                        e.Salary
                    }).ToListAsync();

                    return supervisedEmp;
            }
            else
            {
                var empDetail = await _userManager.Users.Where(e => e.Id == emp!.Id).Select(e => new
                    {
                        Name = $"{e.Fname} {e.Lname}",
                        Address = e.Address,
                        PhoneNumber = e.PhoneNumber,
                        Email = e.Email,
                        Supervisor = $"{e.SupervisorempnoNavigation!.Fname} {e.SupervisorempnoNavigation.Lname}",
                        EmploymentType = e.Employmenttype,
                        Level = e.Level,
                        e.Sex,
                        DOB = e.Dob,
                        e.Salary,
                        SSN = e.Ssn
                    }).ToListAsync();

                    return empDetail;
            }
        }

        public async Task<BaseResponseDto> UpdateExistingEmployee(string empNo, Employee inputEmployee)
        {
            // get user form httpcontext
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            foreach (var empRole in empRoles)
            {
                if (empRole == "Administrator" || empRole == "HR Manager")
                {
                    var empToBeUpdated = await _userManager.FindByIdAsync(empNo);

                    if (empToBeUpdated == null)
                    {
                        return new BaseResponseDto
                        {
                            Status = "Error",
                            Message = "Employee does not exist"
                        };
                    }

                    empToBeUpdated.Address = inputEmployee.Address;
                    empToBeUpdated.Deptno = inputEmployee.Deptno;
                    empToBeUpdated.Dob = inputEmployee.Dob;
                    empToBeUpdated.Email = inputEmployee.Email;
                    empToBeUpdated.Empdependents = inputEmployee.Empdependents;
                    empToBeUpdated.Employmenttype = inputEmployee.Employmenttype;
                    empToBeUpdated.Fname = inputEmployee.Fname;
                    empToBeUpdated.Level = inputEmployee.Level;
                    empToBeUpdated.Lname = inputEmployee.Lname;
                    empToBeUpdated.PhoneNumber = inputEmployee.PhoneNumber;
                    empToBeUpdated.Salary = inputEmployee.Salary;
                    empToBeUpdated.Sex = inputEmployee.Sex;
                    empToBeUpdated.Ssn = inputEmployee.Ssn;

                    await _userManager.UpdateAsync(empToBeUpdated);

                    return new BaseResponseDto
                    {
                        Status = "Success",
                        Message = "Employee updated successfully"
                    };
                }
                else
                {
                    var userId = await _userManager.GetUserIdAsync(emp!);

                    if (userId != empNo)
                    {
                        return new BaseResponseDto
                        {
                            Status = "Error",
                            Message = "You can only update your own profile"
                        };
                    }

                    if (emp!.Ssn != inputEmployee.Ssn || emp.Salary != inputEmployee.Salary)
                    {
                        return new BaseResponseDto
                        {
                            Status = "Error",
                            Message = "Cant update SSN or salary"
                        };
                    }

                    emp!.Address = inputEmployee.Address;
                    emp.Deptno = inputEmployee.Deptno;
                    emp.Dob = inputEmployee.Dob;
                    emp.Email = inputEmployee.Email;
                    emp.Empdependents = inputEmployee.Empdependents;
                    emp.Employmenttype = inputEmployee.Employmenttype;
                    emp.Fname = inputEmployee.Fname;
                    emp.Level = inputEmployee.Level;
                    emp.Lname = inputEmployee.Lname;
                    emp.PhoneNumber = inputEmployee.PhoneNumber;
                    emp.Salary = inputEmployee.Salary;
                    emp.Sex = inputEmployee.Sex;
                    emp.Ssn = inputEmployee.Ssn;

                    await _userManager.UpdateAsync(emp);

                    return new BaseResponseDto
                    {
                        Status = "Success",
                        Message = "Profile updated successfully"
                    };
                }
            }

            return new BaseResponseDto
            {
                Status = "Error",
                Message = "Something went wrong"
            };
        }
    }
}