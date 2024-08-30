using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Persistance.Repository
{
    public class WorksonRepository : IWorksonRepository
    {
        private readonly AppDbContext _context;

        public WorksonRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(Workson workson)
        {
            await _context.Worksons.AddAsync(workson);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Workson workson)
        {
            _context.Worksons.Remove(workson);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Workson>> GetAll(int recordsPerPage, int currentPage)
        {
            var worksons = await _context.Worksons.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();

            return worksons;
        }

        public async Task<IEnumerable<Workson>> GetAllNoPaging()
        {
            var worksons = await _context.Worksons.ToListAsync();

            return worksons;
        }

        public async Task<Workson?> GetById(string empNo, int projNo)
        {
            var workson = await _context.Worksons.FindAsync(empNo, projNo);
            return workson;
        }

        public async Task Update(Workson workson)
        {
            _context.Worksons.Update(workson);
            await _context.SaveChangesAsync();
        }
    }
}