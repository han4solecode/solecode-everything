using HRIS.Application.Contracts;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;

namespace HRIS.Application.Services
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
                await _departmentRepository.Create(department);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteExistingDepartment(int id)
        {
            var department = await _departmentRepository.GetById(id);

            if (department == null)
            {
                return false;
            }

            try
            {
                await _departmentRepository.Delete(department);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Department>> GetAllDepartments(int a, int b)
        {
            var departments = await _departmentRepository.GetAll(a, b);

            return departments;
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetById(id);

            return department;
        }

        public async Task<bool> UpdateExistingDepartment(int id, Department inputDepartment)
        {
            var department = await _departmentRepository.GetById(id);

            if (department == null)
            {
                return false;
            }

            department.Deptname = inputDepartment.Deptname;
            department.Mgrempno = inputDepartment.Mgrempno;
            
            await _departmentRepository.Update(department);

            return true;
        }
    }
}