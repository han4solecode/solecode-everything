using LMS.Application.Contracts;
using LMS.Application.Mappers;
using LMS.Application.Persistance.Helper;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var books = await _bookService.GetAllBooks(recordsPerPage, currentPage);

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var isCreated = await _bookService.AddNewBook(book);

            if (!isCreated)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id}, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book inputBook)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isUpdated = await _bookService.UpdateExistingBook(id, inputBook);

                if (!isUpdated)
                {
                    return BadRequest();
                }

                return Ok(inputBook);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isDeleted = await _bookService.DeleteExistingBook(id);

                if (!isDeleted)
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchBookByQuery([FromQuery] int recordsPerPage, [FromQuery] int currentPage, [FromQuery] QueryObject query)
        {
            var books = await _bookService.SearchBookByQuery(query, recordsPerPage, currentPage);

            return Ok(books);
        }

        [HttpPatch("{id}/softdelete")]
        public async Task<IActionResult> BookSoftDelete(int id, [FromQuery] string? reason)
        {
            var isDeleted = await _bookService.BookSoftDelete(id, reason);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}