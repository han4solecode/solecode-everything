using SLMS.Domain.Entities;

namespace SLMS.Application.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(int userId);

        Task AddUser(User user);

        Task<User?> UpdateUser(int userId, User inputUser);

        Task<bool> DeleteUser(int userId);
    }
}