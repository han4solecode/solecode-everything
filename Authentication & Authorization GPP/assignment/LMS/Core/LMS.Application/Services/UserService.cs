// using LMS.Application.Contracts;
// using LMS.Application.Persistance;
// using LMS.Domain.Entities;

// namespace LMS.Application.Services
// {
//     public class UserService : IUserService
//     {
//         private readonly IUserRepository _userRepository;

//         public UserService(IUserRepository userRepository)
//         {
//             _userRepository = userRepository;
//         }

//         public async Task<bool> AddNewUser(User user)
//         {
//             try
//             {
//                 await _userRepository.Create(user);

//                 // if (user.Position == "Manager" || user.Position == "Librarian")
//                 // {

//                 // }

//                 return true;
//             }
//             catch (System.Exception)
//             {
//                 return false;
//             }
//         }

//         public async Task<bool> DeleteExistingUser(int id)
//         {
//             var user = await _userRepository.GetById(id);

//             if (user == null)
//             {
//                 return false;
//             }

//             try
//             {
//                 await _userRepository.Delete(user);
//                 return true;
//             }
//             catch (System.Exception)
//             {
//                 return false;
//             }
//         }

//         public async Task<IEnumerable<User>> GetAllUsers(int a, int b)
//         {
//             var users = await _userRepository.GetAll(a, b);

//             return users;
//         }

//         public async Task<User?> GetUserById(int id)
//         {
//             var user = await _userRepository.GetById(id);

//             return user;
//         }

//         public async Task<bool> UpdateExistingUser(int id, User inputUser)
//         {
//             var user = await _userRepository.GetById(id);

//             if (user == null)
//             {
//                 return false;
//             }

//             user.FirstName = inputUser.FirstName;
//             user.LastName = inputUser.LastName;
//             user.Email = inputUser.Email;
//             user.Address = inputUser.Address;
//             user.Position = inputUser.Position;

//             await _userRepository.Update(user);

//             return true;
//         }
//     }
// }

using LMS.Application.Contracts;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<object> GetLibraryUserInfo(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var info = new {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    LibraryCardNumber = user.LibraryCard!.CardNumber,
                    LibraryCardExpiryDate = user.LibraryCard!.ExpiryDate,
                    Penalty = user.Penalty,
                    BooksNotReturnedNum = user.Lendings.Count
                };

                return info;
            }

            return new {
                Message = "User not found"
            };
        }
    }
}