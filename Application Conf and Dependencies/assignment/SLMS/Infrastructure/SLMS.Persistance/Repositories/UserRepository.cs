using Microsoft.EntityFrameworkCore;
using SLMS.Application.Repositories;
using SLMS.Domain.Entities;
using SLMS.Persistance.Data;
using SLMS.Persistance.Factories;

namespace SLMS.Persistance.Repositories
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

        public async Task<User?> GetUserById(int userId)
        {
            var user = await _context.Users.Include(u => u.Lendings).FirstOrDefaultAsync(u => u.Userid == userId);

            if (user == null)
            {
                return null;
            }

            return user;
        }
        
        public async Task AddUser(User user)
        {
            var newUser = new UserFactory().CreateItem(user);
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> UpdateUser(int userId, User inputUser)
        {
            var userToBeUpdated = await _context.Users.FindAsync(userId);

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

        public async Task<bool> DeleteUser(int userId)
        {
            var userToBeDeleted = await _context.Users.FindAsync(userId);

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