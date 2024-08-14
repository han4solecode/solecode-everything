using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SLMS.Application.Repositories;
using SLMS.Domain.Entities;
using SLMS.Persistance.Data;

namespace SLMS.Persistance.Repositories
{
    public class LendingRepository : ILendingRepository
    {
        private readonly LibraryContext _context;
        private readonly LibraryOptions _options;

        public LendingRepository(LibraryContext libraryContext, IOptions<LibraryOptions> libraryOptions)
        {
            _context = libraryContext;
            _options = libraryOptions.Value;
        }

        public async Task AddLending(Lending lending)
        {
            await _context.Lendings.AddAsync(lending);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteLending(int lendingId)
        {
            var lendingToBeDeleted = await _context.Lendings.FindAsync(lendingId);

            if (lendingToBeDeleted == null)
            {
                return false;
            }

            _context.Lendings.Remove(lendingToBeDeleted);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Lending>> GetAllLendings()
        {
            var lendings = await _context.Lendings.ToListAsync();

            return lendings;
        }

        public async Task<Lending?> GetLendingById(int lendingId)
        {
            var lending = await _context.Lendings.FindAsync(lendingId);

            if (lending == null)
            {
                return null;
            }

            return lending;
        }

        public async Task<Lending?> UpdateLending(int lendingId, Lending inputLending)
        {
            var lendingToBeUpdated = await _context.Lendings.FindAsync(lendingId);

            if (lendingToBeUpdated == null)
            {
                return null;
            }

            lendingToBeUpdated.Userid = inputLending.Userid;
            lendingToBeUpdated.Bookid = inputLending.Bookid;
            lendingToBeUpdated.Borrowdate = inputLending.Borrowdate;
            lendingToBeUpdated.Returndate = inputLending.Returndate;

            _context.Lendings.Update(lendingToBeUpdated);
            await _context.SaveChangesAsync();

            return lendingToBeUpdated;
        }

        // public async Task<IEnumerable<Lending>?> BorrowBook(int userId, int[] bookIds)
        // {
        //     var user = new UserRepository(_context).GetUserById(userId);

        //     if (user == null)
        //     {
        //         return null;
        //     }

        //     if (bookIds.Length > _options.MaxBorrowedBook)
        //     {
        //         return null;
        //     }

        //     // var availableBooks = new BookRepository(_context).GetAllBooks();

        //     var isBookAvailable = bookIds.All(x => _context.Books.Any(y => y.Bookid == x));

        //     if (!isBookAvailable)
        //     {
        //         return null;
        //     }


        // }
    }
}