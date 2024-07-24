using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void UserInfo()
        {
            Console.WriteLine("User ID: {0} Fullname: {1} {2}", UserId, FirstName, LastName);
        }

    }
}
