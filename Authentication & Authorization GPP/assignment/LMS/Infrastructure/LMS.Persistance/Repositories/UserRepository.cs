using LMS.Application.Persistance;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll(int recordsPerPage, int currentPage)
        {
            var users = await _context.Users.Where(u => u.IsDeleted == false).Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
            return users;
        }

        public async Task<User?> GetById(int id)
        {
            var user = await _context.Users.Where(u => u.IsDeleted == false).SingleOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task Update(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}