using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleLibraryManagementSystemWebAPI.Data;
using SimpleLibraryManagementSystemWebAPI.Interfaces;
using SimpleLibraryManagementSystemWebAPI.Models;
using SimpleLibraryManagementSystemWebAPI.Options;

namespace SimpleLibraryManagementSystemWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _context;
        private readonly LibraryOptions _libraryOptions;

        public UserRepository(LibraryContext libraryContext, IOptions<LibraryOptions> libraryOptions)
        {
            _context = libraryContext;
            _libraryOptions = libraryOptions.Value;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _context.Users.Include(u => u.Lendings).ToListAsync();

            return users;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _context.Users.Include(u => u.Lendings).FirstOrDefaultAsync(u => u.Userid == id);

            return user;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> UpdateUser(int id, User inputUser)
        {
            var userToBeUpdated = await _context.Users.FindAsync(id);

            if (userToBeUpdated == null)
            {
                return null;
            }

            userToBeUpdated.Name = inputUser.Name;
            userToBeUpdated.Email = inputUser.Email;
            userToBeUpdated.Address = inputUser.Address;

            _context.Users.Update(userToBeUpdated);
            await _context.SaveChangesAsync();

            return userToBeUpdated;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userToBeDeleted = await _context.Users.FindAsync(id);

            if (userToBeDeleted == null)
            {
                return false;
            }

            _context.Users.Remove(userToBeDeleted);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Lending?> BorrowBook(Lending lending)
        {
            var user = await _context.Users.FindAsync(lending.Userid);

            if (user == null)
            {
                return null;
            }

            if (lending.Bookid.Count > _libraryOptions.MaxBorrowedBook)
            {
                return null;
            }

            var isBookAvailable = lending.Bookid.All(x => _context.Books.Any(y => y.Bookid == x));

            if (!isBookAvailable)
            {
                return null;
            }

            var lendTransac = new Lending()
            {
                Userid = user.Userid,
                Bookid = [],
                Borrowdate = DateOnly.FromDateTime(DateTime.Now),
                Returndate = DateOnly.FromDateTime(DateTime.Now).AddDays(_libraryOptions.BookLoanDuration),
            };

            lendTransac.Bookid.AddRange(lending.Bookid);
            await _context.Lendings.AddAsync(lendTransac);
            await _context.SaveChangesAsync();

            return lendTransac;
        }

        public async Task ReturnBook(int userId)
        {
            
        }
    }
}