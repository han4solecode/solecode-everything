using System.Security.Claims;
using HRIS.Application.Contracts;
using HRIS.Application.DTOs;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HRIS.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly UserManager<Employee> _userManager;

        public DepartmentService(IDepartmentRepository departmentRepository, UserManager<Employee> userManager)
        {
            _departmentRepository = departmentRepository;
            _userManager = userManager;
        }

        public async Task<bool> AddNewDepartment(Department department)
        {
            try
            {
                await _departmentRepository.Create(department);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var dept = await _departmentRepository.GetById(id);

            if (dept == null)
            {
                return false;
            }

            await _departmentRepository.Delete(dept);
            return true;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments(int a, int b)
        {
            var depts = await _departmentRepository.GetAll(a, b);

            return depts;
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            var dept = await _departmentRepository.GetById(id);

            return dept;
        }

        public async Task<object> GetDepartmentInfo(string userId)
        {
            var emp = await _userManager.FindByIdAsync(userId);

            var empDeptNo = emp!.Deptno!.Value;

            var dept = await _departmentRepository.GetById(empDeptNo);

            return new
            {
                DeptName = dept!.Deptname,
                ManagerName = $"{dept.MgrempnoNavigation!.Fname} {dept.MgrempnoNavigation!.Lname}",
                Employees = dept.Employees.Select(e => new
                {
                    Name = $"{e.Fname} {e.Lname}",
                    Address = e.Address,
                    PhoneNumber = e.PhoneNumber,
                    Email = e.Email,
                    Supervisor = $"{e.SupervisorempnoNavigation!.Fname} {e.SupervisorempnoNavigation.Lname}",
                    EmploymentType = e.Employmenttype
                }),
                Locations = dept.Locations.Select(l => new
                {
                    Address = l.Address
                }),
                Projects = dept.Projects.Select(p => new
                {
                    Name = p.Projname
                })
            };
        }

        public async Task<bool> UpdateExistingDepartment(int id, Department inputDepartment)
        {
            var dept = await _departmentRepository.GetById(id);

            if (dept == null)
            {
                return false;
            }

            dept.Deptname = inputDepartment.Deptname;
            dept.Mgrempno = inputDepartment.Mgrempno;

            await _departmentRepository.Update(dept);

            return true;
        }

        public async Task<bool> UpdateDeptByManager(string userId, Department inputDepartment)
        {
            var emp = await _userManager.FindByIdAsync(userId);

            var empDeptNo = emp!.Deptno!.Value;

            var dept = await _departmentRepository.GetById(empDeptNo);

            try
            {
                dept!.Deptname = inputDepartment.Deptname;
                await _departmentRepository.Update(dept);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<BaseResponseDto> AssignDepartment(string empNo, int deptNo)
        {
            var empToBeAssigned = await _userManager.FindByIdAsync(empNo);
            var dept = await _departmentRepository.GetById(deptNo);

            if (empToBeAssigned == null)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Employee does not exist"
                };
            }

            if (dept == null)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Department does not exist"
                };
            }

            empToBeAssigned.Deptno = deptNo;

            await _userManager.UpdateAsync(empToBeAssigned);

            return new BaseResponseDto
            {
                Status = "Success",
                Message = $"Employee assigned successfully to department {dept.Deptname}"
            };
        }
    }
}