using Microsoft.Extensions.Options;
using SLMS.Application.Repositories;
using SLMS.Domain.Entities;

namespace SLMS.Persistance.Services
{
    public class BookManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILendingRepository _lendingRepository;
        private readonly LibraryOptions _options;


        public BookManager(IUserRepository userRepository, IBookRepository bookRepository, ILendingRepository lendingRepository, IOptions<LibraryOptions> libraryOptions)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _lendingRepository = lendingRepository;
            _options = libraryOptions.Value;
        }

        public async IEnumerable<Lending>? BorrowBook(int userId, int[] bookIds)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return null;
            }

            if (bookIds.Length > _options.MaxBorrowedBook)
            {
                return null;
            }

            foreach (var id in bookIds)
            {
                await _bookRepository.GetBookById(id);
            }
        }
    }
}