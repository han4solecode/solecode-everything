using LMS.Application.Contracts;
using LMS.Application.Persistance;
using LMS.Domain.Entities;

namespace LMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddNewUser(User user)
        {
            try
            {
                await _userRepository.Create(user);

                // if (user.Position == "Manager" || user.Position == "Librarian")
                // {

                // }

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteExistingUser(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return false;
            }

            try
            {
                await _userRepository.Delete(user);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers(int a, int b)
        {
            var users = await _userRepository.GetAll(a, b);

            return users;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);

            return user;
        }

        public async Task<bool> UpdateExistingUser(int id, User inputUser)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return false;
            }

            user.FirstName = inputUser.FirstName;
            user.LastName = inputUser.LastName;
            user.Email = inputUser.Email;
            user.Address = inputUser.Address;
            user.Position = inputUser.Position;

            await _userRepository.Update(user);

            return true;
        }
    }
}