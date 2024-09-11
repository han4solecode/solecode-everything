// using LMS.Domain.Entities;

// namespace LMS.Application.Contracts
// {
//     public interface IUserService
//     {
//         Task<IEnumerable<User>> GetAllUsers(int a, int b);

//         Task<User?> GetUserById(int id);

//         Task<bool> AddNewUser(User user);

//         Task<bool> UpdateExistingUser(int id, User inputUser);

//         Task<bool> DeleteExistingUser(int id);
//     }
// }

using LMS.Domain.Entities;

namespace LMS.Application.Contracts
{
    public interface IUserService
    {
        Task<object> GetLibraryUserInfo(string id);

        Task<object> GetPatronInfoById(string id);

        Task<byte[]> GeneratePatronInfoByIdReport(string id);

        Task<IEnumerable<AppUser>> Get10MostActiveMembers();
    }
}