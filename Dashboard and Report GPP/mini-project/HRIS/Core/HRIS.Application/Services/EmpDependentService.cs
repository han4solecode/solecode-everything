using HRIS.Application.Contracts;
using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Dependent;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HRIS.Application.Services
{
    public class EmpDependentService : IEmpDependentService
    {
        private readonly IEmpDependentRepository _empDependentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Employee> _userManager;

        public EmpDependentService(IEmpDependentRepository empDependentRepository, IHttpContextAccessor httpContextAccessor, UserManager<Employee> userManager)
        {
            _empDependentRepository = empDependentRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<DependentCreateResponseDto> AddNewEmpDependent(EmpDependent empDependent)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            if (empRoles.Any(r => r == "Administrator" || r == "HR Manager"))
            {
                try
                {
                    await _empDependentRepository.Create(empDependent);

                    return new DependentCreateResponseDto
                    {
                        Status = "Success",
                        Message = "Employee dependent created successfully",
                        Data = empDependent
                    };
                }
                catch (System.Exception)
                {
                    return new DependentCreateResponseDto
                    {
                        Status = "Error",
                        Message = "Employee dependent cannot be created"
                    };
                }
            }
            else
            {
                try
                {
                    empDependent.Empno = emp!.Id;
                    await _empDependentRepository.Create(empDependent);

                    return new DependentCreateResponseDto
                    {
                        Status = "Success",
                        Message = "Employee dependent created successfully",
                        Data = empDependent
                    };
                }
                catch (System.Exception)
                {
                    return new DependentCreateResponseDto
                    {
                        Status = "Error",
                        Message = "Employee dependent cannot be created"
                    };
                }
            }
        }

        public async Task<BaseResponseDto> DeleteEmpDependent(int id)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            if (empRoles.Any(r => r == "Administrator" || r == "HR Manager"))
            {
                var empDependent = await _empDependentRepository.GetById(id);

                if (empDependent == null)
                {
                    return new BaseResponseDto
                    {
                        Status = "Error",
                        Message = "Employee dependent does not exist"
                    };
                }

                await _empDependentRepository.Delete(empDependent);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Employee dependent deleted successfully"
                };
            }
            else
            {
                var allEmpDependents = await _empDependentRepository.GetAllNoPaging();

                var empDependents = allEmpDependents.Where(d => d.Empno == emp!.Id);

                var isAvailable = empDependents.Any(d => d.Id == id);

                if (!isAvailable)
                {
                    return new BaseResponseDto
                    {
                        Status = "Error",
                        Message = "Delete dependent request denied. Please check your priviledges"
                    };
                }

                var dependentToBeDeleted = await _empDependentRepository.GetById(id);

                await _empDependentRepository.Delete(dependentToBeDeleted!);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Employee dependent deleted successfully"
                };
            }
        }

        public async Task<IEnumerable<EmpDependent>> GetAllEmpDependents()
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            var allEmpDependents = await _empDependentRepository.GetAllNoPaging();

            if (empRoles.Any(r => r == "Administrator" || r == "HR Manager"))
            {
                return allEmpDependents;
            }
            else
            {
                var empDependents = allEmpDependents.Where(d => d.Empno == emp!.Id);

                return empDependents;
            }
        }

        public async Task<EmpDependent?> GetEmpDependentById(int id)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            if (empRoles.Any(r => r == "Administrator" || r == "HR Manager"))
            {
                var empDependent = await _empDependentRepository.GetById(id);

                return empDependent;
            }
            else
            {
                var allEmpDependents = await _empDependentRepository.GetAllNoPaging();

                var empDependents = allEmpDependents.Where(d => d.Empno == emp!.Id);

                var isAvailable = empDependents.Any(d => d.Id == id);

                if (isAvailable)
                {
                    var empDependent = await _empDependentRepository.GetById(id);

                    return empDependent;
                }

                return null;
            }
        }

        public async Task<BaseResponseDto> UpdateExistingEmpDependent(int id, EmpDependent inputEmpDependet)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;

            var emp = await _userManager.FindByNameAsync(userName!);

            var empRoles = await _userManager.GetRolesAsync(emp!);

            if (empRoles.Any(r => r == "Administrator" || r == "HR Manager"))
            {
                var empDependent = await _empDependentRepository.GetById(id);

                if (empDependent == null)
                {
                    return new BaseResponseDto
                    {
                        Status = "Errpr",
                        Message = "Employee dependent does not exist"
                    };
                }

                empDependent.Name = inputEmpDependet.Name;
                empDependent.Dob = inputEmpDependet.Dob;
                empDependent.Sex = inputEmpDependet.Sex;
                empDependent.Relationship = inputEmpDependet.Relationship;

                await _empDependentRepository.Update(empDependent);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Employee dependent updated successfully"
                };
            }
            else
            {
                var allEmpDependents = await _empDependentRepository.GetAllNoPaging();

                var empDependents = allEmpDependents.Where(d => d.Empno == emp!.Id);

                var isAvailable = empDependents.Any(d => d.Id == id);

                if (!isAvailable)
                {
                    return new BaseResponseDto
                    {
                        Status = "Error",
                        Message = "Update dependent request denied. Please check your priviledges"
                    };
                }

                var empDependent = await _empDependentRepository.GetById(id);

                empDependent!.Name = inputEmpDependet.Name;
                empDependent.Dob = inputEmpDependet.Dob;
                empDependent.Sex = inputEmpDependet.Sex;
                empDependent.Relationship = inputEmpDependet.Relationship;

                await _empDependentRepository.Update(empDependent);

                return new BaseResponseDto
                {
                    Status = "Success",
                    Message = "Employee dependent updated successfully"
                };
            }
        }
    }
}