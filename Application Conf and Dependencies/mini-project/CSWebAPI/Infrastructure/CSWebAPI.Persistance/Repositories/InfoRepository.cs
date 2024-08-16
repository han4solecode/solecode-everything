using CSWebAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CSWebAPI.Persistance.Repositories
{
    public class InfoRepository : IInfoRepository
    {
        private readonly AppDbContext _context;

        public InfoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<object>> EmpAge()
        {
            var employee = await _context.Employees.Include(e => e.DeptnoNavigation).Select(e => new
            {
                Name = $"{e.Fname} {e.Lname}",
                Department = e.DeptnoNavigation.Deptname,
                Age = DateOnly.FromDateTime(DateTime.Now).Year - e.Dob.Year
            }).ToListAsync();

            return employee;
        }

        public async Task<IEnumerable<object>> EmpNotManagerAndSupervisor()
        {
            var employee = await _context.Employees.Where(e => !e.Position.Contains("Manager") && !e.Position.Contains("Supervisor")).Select(e => new
            {
                e.Fname,
                e.Lname,
                e.Position,
                e.Sex,
                e.Deptno
            }).ToListAsync();

            return employee;
        }

        public async Task<IEnumerable<object>> EmpProjInfo()
        {
            var empProjInfo = await (from e in _context.Employees
                                     join w in _context.Worksons
                                     on e.Empno equals w.Empno
                                     select w).GroupBy(a => a.Empno).Select(b => new
                                     {
                                         Name = b.Select(c => c.EmpnoNavigation.Fname + " " + c.EmpnoNavigation.Lname).FirstOrDefault(),
                                         Project = b.Select(c => c.ProjnoNavigation.Projname).ToList(),
                                         TotalHoursWorked = b.Select(c => c.Hoursworked).Sum()
                                     }).ToListAsync();

            return empProjInfo;
        }

        public async Task<IEnumerable<object>> EmpTotalWorkHours()
        {
            var empTotalWorkHours = await (from e in _context.Employees
                                           join w in _context.Worksons
                                            on e.Empno equals w.Empno
                                           select w).GroupBy(a => a.Empno).Select(b => new
                                           {
                                               Name = b.Select(c => c.EmpnoNavigation.Fname + " " + c.EmpnoNavigation.Lname).FirstOrDefault(),
                                               TotalHoursWorked = b.Select(c => c.Hoursworked).Sum()
                                           }).ToListAsync();

            return empTotalWorkHours;
        }

        public async Task<IEnumerable<object>> FemaleManagerProjects()
        {
            var femaleManagerProjects = await (from e in _context.Employees
                                     join w in _context.Worksons
                                     on e.Empno equals w.Empno
                                     where e.Sex == "Female"
                                     where e.Empno == e.Department.Mgrempno
                                     select w).GroupBy(a => a.Empno).Select(b => new
                                     {
                                         Name = b.Select(c => c.EmpnoNavigation.Fname + " " + c.EmpnoNavigation.Lname).FirstOrDefault(),
                                         Department = b.Select(c => c.EmpnoNavigation.DeptnoNavigation.Deptname).FirstOrDefault(),
                                         Project = b.Select(c => c.ProjnoNavigation.Projname).ToList(),
                                     }).ToListAsync();

            return femaleManagerProjects;
        }

        public async Task<IEnumerable<object>> FemEmpHoursWorked()
        {
            var femEmpHoursWorked = await (from e in _context.Employees
                                            join w in _context.Worksons
                                            on e.Empno equals w.Empno
                                            where e.Sex == "Female" orderby e.Lname
                                            select w).GroupBy(a => a.EmpnoNavigation.Deptno).Select(b => new {
                                                DeptNo = b.Key,
                                                DeptName = b.Select(c => c.ProjnoNavigation.DeptnoNavigation.Deptname).FirstOrDefault(),
                                                Name = b.Select(c => c.EmpnoNavigation.Fname + " " + c.EmpnoNavigation.Lname).FirstOrDefault(),
                                                TotalHoursWorked = b.Select(c => c.Hoursworked).Sum()
                                            }).OrderBy(a => a.DeptNo).ToListAsync();
            
            return femEmpHoursWorked;
        }

        public async Task<IEnumerable<object>> ITDeptEmployees()
        {
            var employees = await _context.Employees.Include(e => e.DeptnoNavigation).Where(e => e.DeptnoNavigation.Deptname == "IT").Select(e => new
            {
                Name = $"{e.Fname} {e.Lname}",
                Address = e.Address
            }).ToListAsync();

            return employees;
        }

        public async Task<IEnumerable<object>> ListDeptWithMore10Emp()
        {
            var departments = await _context.Departments.Where(d => d.Employees.Count() > 10).Select(d => new
            {
                DeptName = d.Deptname,
                EmpCount = d.Employees.Count()
            }).ToListAsync();

            return departments;
        }

        public async Task<IEnumerable<object>> ManagerUnder40()
        {
            var managers = await _context.Employees.Where(e => e.Empno == e.Department.Mgrempno && (DateOnly.FromDateTime(DateTime.Now).Year - e.Dob.Year) < 40).Select(m => new
            {
                Name = $"{m.Fname} {m.Lname}",
                Age = DateOnly.FromDateTime(DateTime.Now).Year - m.Dob.Year
            }).ToListAsync();

            return managers;
        }

        public async Task<object> MaxAndMinWorkHours()
        {
            var maxWorkHours = await _context.Worksons.Select(w => w.Hoursworked).MaxAsync();
            var minWorkHours = await _context.Worksons.Select(w => w.Hoursworked).MinAsync();

            Object workHours = new { maxWorkHours, minWorkHours };

            return workHours;
        }
    }
}