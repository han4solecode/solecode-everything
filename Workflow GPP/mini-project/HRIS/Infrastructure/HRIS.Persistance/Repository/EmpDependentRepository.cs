using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Persistance.Repository
{
    public class EmpDependentRepository : IEmpDependentRepository
    {
        private readonly AppDbContext _context;
        
        public EmpDependentRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(EmpDependent entity)
        {
            await _context.EmpDependents.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(EmpDependent entity)
        {
            _context.EmpDependents.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmpDependent>> GetAll(int recordsPerPage, int currentPage)
        {
            var empDependents = await _context.EmpDependents.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();

            return empDependents;
        }

        public async Task<IEnumerable<EmpDependent>> GetAllNoPaging()
        {
            var empDependents = await _context.EmpDependents.ToListAsync();

            return empDependents;
        }

        public async Task<EmpDependent?> GetById(int id)
        {
            var empDependent = await _context.EmpDependents.FindAsync(id);

            return empDependent;
        }

        public async Task Update(EmpDependent entity)
        {
            _context.EmpDependents.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}