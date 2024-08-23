using HRIS.Application.Persistance;
using HRIS.Application.Persistance.Helper;
using HRIS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Persistance.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrisContext _context;

        public EmployeeRepository(HrisContext hrisContext)
        {
            _context = hrisContext;
        }

        public async Task Create(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateEmployee(Employee employee, string? reason)
        {
            employee.Status = "Not Active";
            employee.Deactreason = reason;
            employee.DeactivatedAt = DateOnly.FromDateTime(DateTime.Now);

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Employee entity)
        {
            _context.Employees.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAll(int recordsPerPage, int currentPage)
        {
            var employees = await _context.Employees.Where(e => e.Status == "Active").Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();

            return employees;
        }

        public async Task<Employee?> GetById(int id)
        {
            var employee = await _context.Employees.Where(e => e.Status == "Active").SingleOrDefaultAsync(e => e.Empno == id);

            return employee;
        }

        public async Task<IEnumerable<object>> GetDetail(int id)
        {
            var employee = await _context.Employees.Where(e => e.Status == "Active" && e.Empno == id).Select(e => new {
                Name = $"{e.Fname} {e.Lname}",
                Address = e.Address,
                PhoneNumber = e.Phonenumber,
                Email = e.Email,
                Position = e.Position,
                Supervisor = $"{e.SupervisorempnoNavigation!.Fname} {e.SupervisorempnoNavigation.Lname}",
                EmploymentType = e.Employmenttype
            }).ToListAsync();

            return employee;
        }

        public async Task<IEnumerable<object>> SearchEmployee(EmployeeQueryObject query, EmployeeSortObject sort, int recordsPerPage, int currentPage)
        {
            if (query.GetType().GetProperties().Select(q => q.GetValue(query)).All(val => val == null))
            {
                var emps = await _context.Employees.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).Select(e => new
                {
                    Name = $"{e.Fname} {e.Lname}",
                    Department = e.DeptnoNavigation!.Deptname,
                    Position = e.Position,
                    Level = e.Level,
                    EmployementType = e.Employmenttype,
                    LastUpdated = e.UpdatedAt
                }).OrderBy(e => e.Name).ToListAsync();
            }

            var employees = _context.Employees.AsQueryable();

            var res = employees.Where(e =>
                ((!string.IsNullOrEmpty(query.DepartmentName) && e.DeptnoNavigation!.Deptname.ToLower().Contains(query.DepartmentName.ToLower())) ||
                (!string.IsNullOrEmpty(query.Employmenttype) && e.Employmenttype.ToLower().Contains(query.Employmenttype.ToLower())) ||
                (query.Level != null && e.Level == query.Level) ||
                (!string.IsNullOrEmpty(query.Name) && $"{e.Fname} {e.Lname}".Contains(query.Name)) ||
                (!string.IsNullOrEmpty(query.Position) && e.Position.ToLower().Contains(query.Position.ToLower()))) && e.Status == "Active").Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage);

            if (!string.IsNullOrEmpty(sort.SortBy))
            {
                if (sort.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    res = sort.IsDescending ? res.OrderByDescending(e => e.Fname) : res.OrderBy(e => e.Fname);
                }
                else if (sort.SortBy.Equals("Department Name", StringComparison.OrdinalIgnoreCase))
                {
                    res = sort.IsDescending ? res.OrderByDescending(e => e.DeptnoNavigation!.Deptname) : res.OrderBy(e => e.DeptnoNavigation!.Deptname);
                }
                else if (sort.SortBy.Equals("Employment Type", StringComparison.OrdinalIgnoreCase))
                {
                    res = sort.IsDescending ? res.OrderByDescending(e => e.Employmenttype) : res.OrderBy(e => e.Employmenttype);
                }
                else if (sort.SortBy.Equals("Level", StringComparison.OrdinalIgnoreCase))
                {
                    res = sort.IsDescending ? res.OrderByDescending(e => e.Level) : res.OrderBy(e => e.Level);
                }
                else if (sort.SortBy.Equals("Position", StringComparison.OrdinalIgnoreCase))
                {
                    res = sort.IsDescending ? res.OrderByDescending(e => e.Position) : res.OrderBy(e => e.Position);
                }
                else if (sort.SortBy.Equals("Last Updated", StringComparison.OrdinalIgnoreCase))
                {
                    res = sort.IsDescending ? res.OrderByDescending(e => e.UpdatedAt) : res.OrderBy(e => e.UpdatedAt);
                }
            }

            var result = await res.Select(e => new
            {
                Name = $"{e.Fname} {e.Lname}",
                Department = e.DeptnoNavigation!.Deptname,
                Position = e.Position,
                Level = e.Level,
                EmployementType = e.Employmenttype,
                LastUpdated = e.UpdatedAt
            }).ToListAsync();

            return result;

        }

        public async Task Update(Employee entity)
        {
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}