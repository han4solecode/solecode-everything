using SimpleLibraryManagementSystemWebAPI.Interfaces;
using SimpleLibraryManagementSystemWebAPI.Models;

namespace SimpleLibraryManagementSystemWebAPI.Services
{
    public class BookManager
    {
        private static readonly Lazy<BookManager> lazyInstance = new Lazy<BookManager>(() => new BookManager());

        private BookManager()
        {

        }

        public static BookManager Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        // private readonly IUserRepository _userRepository;

        // public BookManager(IUserRepository userRepository)
        // {
        //     _userRepository = userRepository;
        // }

        public void BorrowBook(IUserRepository userRepository)
        {
            
        }

        public void ReturnBook()
        {

        }
    }
}