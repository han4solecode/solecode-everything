using Microsoft.Extensions.Options;
using SLMS.Application.Repositories;
using SLMS.Domain.DTOs;
using SLMS.Domain.Entities;

namespace SLMS.Persistance.Services
{
    public class BookManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILendingRepository _lendingRepository;
        // private readonly LibraryOptions _options;


        public BookManager(IUserRepository userRepository, IBookRepository bookRepository, ILendingRepository lendingRepository)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _lendingRepository = lendingRepository;
            // _options = options.Value;
        }

        public async Task<IEnumerable<Lending>?> BorrowBook(LendingDto lending, int MaxBorrowedBook, int BookLoanDuration)
        {
            // check if user exist using user repo get user by id method
            var user = await _userRepository.GetUserById(lending.UserId);

            if (user == null)
            {
                return null;
            }

            // make sure the maximum borrowed book is 3
            if (lending.BookIds.Length > MaxBorrowedBook)
            {
                return null;
            }

            // check if books is available to borrow, if not return null
            var availableBooks = await _bookRepository.GetAllBooks();

            var isBookAvailable = lending.BookIds.All(x => availableBooks.Any(y => y.Bookid == x));

            if (!isBookAvailable)
            {
                return null;
            }

            List<Lending> input = [];

            foreach (var id in lending.BookIds)
            {
                var lendTransac = new Lending
                {
                    Userid = user.Userid,
                    Bookid = id,
                    Borrowdate = DateOnly.FromDateTime(DateTime.Now),
                    Returndate = DateOnly.FromDateTime(DateTime.Now).AddDays(BookLoanDuration) // return date is calculated by adding book loan duration to the borrow date
                };

                input.Add(lendTransac);
            }

            await _lendingRepository.AddLending(input);

            return input;
        }

        public async Task<bool> ReturnBook(int lendingId)
        {
            var isRemoved = await _lendingRepository.DeleteLending(lendingId);

            return isRemoved;
        }
    }
}