using CompanySystemWebAPI.Data;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanySystemWebAPI.Services
{
    public class WorksonService : IWorksonService
    {
        private readonly AppDbContext _context;

        public WorksonService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Workson>> GetAllWorkson(int recordsPerPage, int currentPage)
        {
            var worksons = await _context.Worksons.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
            return worksons;
        }

        public async Task<Workson?> GetWorksonById(int id)
        {
            var workson = await _context.Worksons.FindAsync(id);
            return workson;
        }

        public async Task AddWorkson(Workson workson)
        {
            await _context.Worksons.AddAsync(workson);
            await _context.SaveChangesAsync();
        }

        public async Task<Workson?> UpdateWorkson(int id, Workson inputWorkson)
        {
            var worksonToBeUpdated = await _context.Worksons.FindAsync(id);

            if (worksonToBeUpdated == null)
            {
                return null;
            }

            worksonToBeUpdated.Empno = inputWorkson.Empno;
            worksonToBeUpdated.Projno = inputWorkson.Projno;
            worksonToBeUpdated.Dateworked = inputWorkson.Dateworked;
            worksonToBeUpdated.Hoursworked = inputWorkson.Hoursworked;

            _context.Worksons.Update(worksonToBeUpdated);
            await _context.SaveChangesAsync();
            return worksonToBeUpdated;
        }

        public async Task<bool> DeleteWorkson(int id)
        {
            var worksonToBeDeleted = await _context.Worksons.FindAsync(id);

            if (worksonToBeDeleted != null)
            {
                _context.Worksons.Remove(worksonToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}