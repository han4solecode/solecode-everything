using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SLMS.Application.Repositories;
using SLMS.Domain.DTOs;
using SLMS.Persistance;
using SLMS.Persistance.Services;

namespace SLMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LendController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly LibraryOptions _options;
        private readonly IBookRepository _bookRepository;
        private readonly ILendingRepository _lendingRepository;

        public LendController(ILendingRepository lendingRepository, IUserRepository userRepository, IBookRepository bookRepository, IOptions<LibraryOptions> libraryOptions)
        {
            _lendingRepository = lendingRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _options = libraryOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook([FromBody] LendingDto lending)
        {
            try
            {
                var lend = await new BookManager(_userRepository, _bookRepository, _lendingRepository).BorrowBook(lending, _options.MaxBorrowedBook, _options.BookLoanDuration);

                if (lend == null)
                {
                    return BadRequest();
                }

                return Ok(lend);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{lendingId}")]
        public async Task<IActionResult> ReturnBook(int lendingId)
        {
            try
            {
                var isReturned = await new BookManager(_userRepository, _bookRepository, _lendingRepository).ReturnBook(lendingId);

                if (!isReturned)
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}