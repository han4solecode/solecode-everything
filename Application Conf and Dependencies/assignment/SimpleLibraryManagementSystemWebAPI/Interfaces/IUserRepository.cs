using SimpleLibraryManagementSystemWebAPI.Models;

namespace SimpleLibraryManagementSystemWebAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(int id);

        Task AddUser(User user);

        Task<User?> UpdateUser(int id, User inputUser);

        Task<bool> DeleteUser(int id);

        // Task<IEnumerable<Lending>?> BorrowBook(int userId, List<int> books);

        // Task ReturnBook(int userId);
    }
}