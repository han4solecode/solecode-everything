using Microsoft.EntityFrameworkCore;
using SimpleLibraryManagementSystemWebAPI.Data;
using SimpleLibraryManagementSystemWebAPI.Interfaces;
using SimpleLibraryManagementSystemWebAPI.Models;

namespace SimpleLibraryManagementSystemWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _context;

        public UserRepository(LibraryContext libraryContext)
        {
            _context = libraryContext;
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
    }
}