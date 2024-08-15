using CSWebAPI.Application.Repositories;
using CSWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Persistance.Repositories
{
    public class WorksonRepository : IWorksonRepository
    {
        private readonly AppDbContext _context;
        private readonly CompanyOptions _options;

        public WorksonRepository(AppDbContext appDbContext, IOptions<CompanyOptions> options)
        {
            _context = appDbContext;
            _options = options.Value;
        }

        public async Task AddWorkson(Workson workson)
        {
            var totalHoursWorkedInProj = await _context.Worksons.Where(w => w.Projno == workson.Projno).Select(w => w.Hoursworked).SumAsync();
            var assignedEmpCount = await _context.Worksons.Where(w => w.Empno == workson.Empno).CountAsync();

            // check if total workin hours in a project is less than 600 hours and the assigned employee is handling less than 3 projects
            if (totalHoursWorkedInProj < _options.MaxWorkingHours && assignedEmpCount < _options.MaxEmpHandleProject)
            {
                await _context.Worksons.AddAsync(workson);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteWorkson(int empNo, int projNo)
        {
            var worksonToBeDeleted = await _context.Worksons.FindAsync(empNo, projNo);

            if (worksonToBeDeleted != null)
            {
                _context.Worksons.Remove(worksonToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Workson>> GetAllWorkson(int recordsPerPage, int currentPage)
        {
            var worksons = await _context.Worksons.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
            return worksons;
        }

        public async Task<Workson?> GetWorksonById(int empNo, int projNo)
        {
            var workson = await _context.Worksons.FindAsync(empNo, projNo);
            return workson;
        }

        public async Task<Workson?> UpdateWorkson(int empNo, int projNo, Workson inputWorkson)
        {
            var worksonToBeUpdated = await _context.Worksons.FindAsync(empNo, projNo);

            if (worksonToBeUpdated == null)
            {
                return null;
            }

            worksonToBeUpdated.Empno = inputWorkson.Empno;
            worksonToBeUpdated.Projno = inputWorkson.Projno;
            worksonToBeUpdated.Dateworked = inputWorkson.Dateworked;
            worksonToBeUpdated.Hoursworked = inputWorkson.Hoursworked;

            var totalHoursWorkedInProj = await _context.Worksons.Where(w => w.Projno == worksonToBeUpdated.Projno).Select(w => w.Hoursworked).SumAsync();
            var assignedEmpCount = await _context.Worksons.Where(w => w.Empno == worksonToBeUpdated.Empno).CountAsync();

            if (totalHoursWorkedInProj < _options.MaxWorkingHours && assignedEmpCount < _options.MaxEmpHandleProject)
            {
                _context.Worksons.Update(worksonToBeUpdated);
                await _context.SaveChangesAsync();
                return worksonToBeUpdated;
            }

            return null;
        }
    }
}