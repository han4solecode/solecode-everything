using System.Security.Cryptography.X509Certificates;
using HRIS.Application.Contracts;
using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Worksons;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Application.Services
{
    public class WorksonService : IWorksonService
    {
        private readonly IWorksonRepository _worksonRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Employee> _userManager;

        public WorksonService(IWorksonRepository worksonRepository, IHttpContextAccessor httpContextAccessor, UserManager<Employee> userManager)
        {
            _worksonRepository = worksonRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        // accessible for admin and employee supervisor
        public async Task<WorksonCreateResponseDto> AddNewWorkson(Workson workson)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            if (empRoles.Any(r => r == "Administrator"))
            {
                try
                {
                    await _worksonRepository.Create(workson);
                    return new WorksonCreateResponseDto
                    {
                        Status = "Success",
                        Message = "Workson created successfully",
                        Data = workson
                    };
                }
                catch (System.Exception)
                {
                    return new WorksonCreateResponseDto
                    {
                        Status = "Error",
                        Message = "Workson cannot be created"
                    };
                }
            }
            else
            {
                var supervisedEmpUserId = await _userManager.Users.Where(e => e.Supervisorempno == emp!.Id && e.Status == "Active").Select(e => e.Id).ToListAsync();

                var isSupervised = supervisedEmpUserId.Any(x => x == workson.Empno);

                if (!isSupervised)
                {
                    return new WorksonCreateResponseDto
                    {
                        Status = "Error",
                        Message = "Create workson request denied"
                    };
                }

                try
                {
                    await _worksonRepository.Create(workson);
                    return new WorksonCreateResponseDto
                    {
                        Status = "Success",
                        Message = "Workson created successfully",
                        Data = workson
                    };
                }
                catch (System.Exception)
                {
                    return new WorksonCreateResponseDto
                    {
                        Status = "Error",
                        Message = "Workson cannot be created"
                    };
                }
            }
        }

        // accessible for admin and employee supervisor
        public async Task<BaseResponseDto> DeleteWorkson(string empNo, int projNo)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);


            if (empRoles.Any(r => r == "Administrator"))
            {
                var worksonToBeDeleted = await _worksonRepository.GetById(empNo, projNo);

                if (worksonToBeDeleted == null)
                {
                    return new BaseResponseDto
                    {
                        Status = "Error",
                        Message = "Workson does not exist"
                    };
                }

                await _worksonRepository.Delete(worksonToBeDeleted);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Workson deleted successfully"
                };
            }
            else
            {
                var supervisedEmpUserId = await _userManager.Users.Where(e => e.Supervisorempno == emp!.Id && e.Status == "Active").Select(e => e.Id).ToListAsync();

                var isSupervised = supervisedEmpUserId.Any(x => x == empNo);

                if (!isSupervised)
                {
                    return new WorksonCreateResponseDto
                    {
                        Status = "Error",
                        Message = "Delete workson request denied"
                    };
                }

                var worksonToBeDeleted = await _worksonRepository.GetById(empNo, projNo);

                if (worksonToBeDeleted == null)
                {
                    return new BaseResponseDto
                    {
                        Status = "Error",
                        Message = "Workson does not exist"
                    };
                }

                await _worksonRepository.Delete(worksonToBeDeleted);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Workson deleted successfully"
                };
            }
        }

        // accessible for admin, employee supervisor, and employee
        public async Task<IEnumerable<object>> GetAllWorksons()
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            var allWorksons = await _worksonRepository.GetAllNoPaging();

            if (empRoles.Any(r => r == "Administrator"))
            {
                return allWorksons;
            }
            else if (empRoles.Any(r => r == "Employee Supervisor"))
            {
                var supervisedEmpUserId = await _userManager.Users.Where(e => e.Supervisorempno == emp!.Id && e.Status == "Active").Select(e => e.Id).ToListAsync();

                var supervisedEmpWorkson = allWorksons.Where(w => supervisedEmpUserId.Contains(w.Empno)).Select(w => new
                {
                    EmpNo = w.Empno,
                    Name = $"{w.EmpnoNavigation.Fname} {w.EmpnoNavigation.Lname}",
                    ProjNo = w.Projno,
                    ProjectName = w.ProjnoNavigation.Projname
                }).ToList();

                return supervisedEmpWorkson;
            }

            var worksons = allWorksons.Where(w => w.Empno == emp!.Id).Select(w => new
            {
                ProjNo = w.Projno,
                ProjectName = w.ProjnoNavigation.Projname
            }).ToList();

            return worksons;
        }

        // accessible for admin and employee supervisor
        public async Task<object> GetWorksonById(string empNo, int projNo)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            if (empRoles.Any(r => r == "Administrator"))
            {
                var workson = await _worksonRepository.GetById(empNo, projNo);

                if (workson == null)
                {
                    return null!;
                }

                return workson;
            }

            var supervisedEmpUserId = await _userManager.Users.Where(e => e.Supervisorempno == emp!.Id && e.Status == "Active").Select(e => e.Id).ToListAsync();

            if (!supervisedEmpUserId.Any(x => x == empNo))
            {
                return null!;
            }

            var asd = await _worksonRepository.GetById(empNo, projNo);

            if (asd == null)
            {
                return null!;
            }

            return asd;

        }

        // accessible for admin, department manager, and employee supervisor
        public async Task<BaseResponseDto> UpdateExistingWorkson(string empNo, int projNo, Workson inputWorkson)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            if (empRoles.Any(r => r == "Administrator"))
            {
                var woToBeUpdated = await _worksonRepository.GetById(empNo, projNo);

                if (woToBeUpdated == null)
                {
                    return new BaseResponseDto
                    {
                        Status = "Error",
                        Message = "Workson does not exist"
                    };
                }

                woToBeUpdated.Empno = inputWorkson.Empno;
                woToBeUpdated.Projno = inputWorkson.Projno;
                woToBeUpdated.Dateworked = inputWorkson.Dateworked;
                woToBeUpdated.Hoursworked = inputWorkson.Hoursworked;

                await _worksonRepository.Update(woToBeUpdated);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Workson updated successfully"
                };
            }
            else if (empRoles.Any(r => r == "Department Manager"))
            {
                // check if workson empNo is in department manager dept
                var mgrDeptNo = emp!.Deptno;
                var empsUserIdInMgrDept = await _userManager.Users.Where(e => e.Deptno == mgrDeptNo && e.Status == "Active").Select(e => e.Id).ToListAsync();

                var isInDept = empsUserIdInMgrDept.All(x => x == empNo);

                if (!isInDept)
                {
                    return new BaseResponseDto
                    {
                        Status = "Error",
                        Message = "Update workson request denied"
                    };
                }

                var x = await _worksonRepository.GetById(empNo, projNo);

                if (x == null)
                {
                    return new BaseResponseDto
                    {
                        Status = "Error",
                        Message = "Workson does not exist"
                    };
                }

                x.Empno = inputWorkson.Empno;
                x.Projno = inputWorkson.Projno;
                x.Dateworked = inputWorkson.Dateworked;
                x.Hoursworked = inputWorkson.Hoursworked;

                await _worksonRepository.Update(x);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Workson updated successfully"
                };
            }

            var supervisedEmpUserId = await _userManager.Users.Where(e => e.Supervisorempno == emp!.Id && e.Status == "Active").Select(e => e.Id).ToListAsync();

            var isSupervised = supervisedEmpUserId.Any(x => x == empNo);

            if (!isSupervised)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Update workson request denied"
                };
            }

            var worksonToBeUpdated = await _worksonRepository.GetById(empNo, projNo);

            if (worksonToBeUpdated == null)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Workson does not exist"
                };
            }

            worksonToBeUpdated.Empno = inputWorkson.Empno;
            worksonToBeUpdated.Projno = inputWorkson.Projno;
            worksonToBeUpdated.Dateworked = inputWorkson.Dateworked;
            worksonToBeUpdated.Hoursworked = inputWorkson.Hoursworked;

            await _worksonRepository.Update(worksonToBeUpdated);

            return new BaseResponseDto
            {
                Status = "Success",
                Message = "Workson updated successfully"
            };
        }
    }
}