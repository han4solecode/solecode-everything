using SLMS.Domain.Entities;

namespace SLMS.Persistance.Factories
{
    public class UserFactory
    {
        public User CreateItem(User userData)
        {
            return new User{
                Name = userData.Name,
                Email = userData.Email,
                Address = userData.Address
            };
        }
    }
}