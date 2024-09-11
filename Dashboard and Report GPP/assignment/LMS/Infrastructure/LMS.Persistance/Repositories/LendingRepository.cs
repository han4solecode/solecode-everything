using LMS.Application.Persistance;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistance.Repositories
{
    public class LendingRepository : ILendingRepository
    {
        private readonly AppDbContext _context;

        public LendingRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(Lending entity)
        {
            await _context.Lendings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Lending entity)
        {
            _context.Lendings.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lending>> GetAll(int recordsPerPage, int currentPage)
        {
            var lendings = await _context.Lendings.Where(l => l.IsDeleted == false).Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();

            return lendings;
        }

        public async Task<Lending?> GetById(int id)
        {
            var lending = await _context.Lendings.Where(l => l.IsDeleted == false).SingleOrDefaultAsync(l => l.Id == id);

            return lending;
        }

        public async Task Update(Lending entity)
        {
            _context.Lendings.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<object> GetUserLendingInfo(string id)
        {
            var userLendings = await _context.Lendings.Where(l => l.AppUserId == id).Select(l => new {
                Title = l.Book!.Title,
                ISBN = l.Book.ISBN,
                BorrowDate = l.BorrowDate,
                DueReturnDate = l.DueReturnDate,
                Branch = "Branch"
            }).ToListAsync();

            return userLendings;
        }

        public async Task<IEnumerable<Lending>> GetAllNoPaging()
        {
            var lendings = await _context.Lendings.ToListAsync();

            return lendings;
        }

        public async Task<IEnumerable<Lending>> GetUsersOverdueLendings()
        {
            var userOverdueLendings = await _context.Lendings.Where(l => l.DueReturnDate < DateOnly.FromDateTime(DateTime.Now) && l.IsDeleted == false && l.DateReturned == null).ToListAsync();

            return userOverdueLendings;
        }
    }
}