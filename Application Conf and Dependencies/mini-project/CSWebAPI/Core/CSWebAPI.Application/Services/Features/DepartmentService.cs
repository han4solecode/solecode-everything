using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services.Interfaces;
using CSWebAPI.Domain.Entities;

namespace CSWebAPI.Application.Services.Features
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> AddNewDepartment(Department department)
        {
            try
            {
                await _departmentRepository.AddDepartment(department);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var dept = await _departmentRepository.GetDepartmentById(id);

            if (dept == null)
            {
                return false;
            }

            await _departmentRepository.DeleteDepartment(dept);
            return true;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments(int a, int b)
        {
            var depts = await _departmentRepository.GetAllDepartment(a, b);

            return depts;
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            var dept = await _departmentRepository.GetDepartmentById(id);

            return dept;
        }

        public async Task<bool> UpdateExistingDepartment(int id, Department inputDepartment)
        {
            var dept = await _departmentRepository.GetDepartmentById(id);

            if (dept == null)
            {
                return false;
            }

            dept.Deptname = inputDepartment.Deptname;
            dept.Mgrempno = inputDepartment.Mgrempno;

            await _departmentRepository.UpdateDepartment(dept);

            return true;
        }
    }
}