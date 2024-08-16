using Microsoft.AspNetCore.Mvc;
using SLMS.Application.Repositories;
using SLMS.Domain.Entities;

namespace SLMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _bookRepository.GetAllBooks();

            return Ok(books);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetById(int bookId)
        {
            if (bookId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var book = await _bookRepository.GetBookById(bookId);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            try
            {
                await _bookRepository.AddBook(book);

                return Created($"api/user/{book.Bookid}", book);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> Put(int bookId, [FromBody] Book inputBook)
        {
            if (bookId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var bookUpdated = await _bookRepository.UpdateBook(bookId, inputBook);

                if (bookUpdated == null)
                {
                    return NotFound();
                }

                return Ok(bookUpdated);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Delete(int bookId)
        {
            if (bookId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isDeleted = await _bookRepository.DeleteBook(bookId);

                if (!isDeleted)
                {
                    return NotFound();
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