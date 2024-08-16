using CSWebAPI.Application.Repositories;
using CSWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CSWebAPI.Persistance.Repositories
{
    public class WorksonRepository : IWorksonRepository
    {
        private readonly AppDbContext _context;

        public WorksonRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddWorkson(Workson workson)
        {
            await _context.Worksons.AddAsync(workson);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkson(Workson workson)
        {
            _context.Worksons.Remove(workson);
            await _context.SaveChangesAsync();
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

        public async Task UpdateWorkson(Workson inputWorkson)
        {
            _context.Worksons.Update(inputWorkson);
            await _context.SaveChangesAsync();
        }

        public async Task<int> TotalHoursWorkedInProject(int projNo)
        {
            var totalHoursWorkedInProj = await _context.Worksons.Where(w => w.Projno == projNo).Select(w => w.Hoursworked).SumAsync();

            return totalHoursWorkedInProj;
        }

        public async Task<int> AssignedEmployeeCount(int empNo)
        {
            var assignedEmpCount = await _context.Worksons.Where(w => w.Empno == empNo).CountAsync();

            return assignedEmpCount;
        }
    }
}