using LMS.Application.Contracts;
using LMS.Application.DTOs.Book;
using LMS.Application.DTOs.Request;
using LMS.Application.Mappers;
using LMS.Application.Persistance.Helper;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Library User, Librarian")]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var books = await _bookService.GetAllBooks(recordsPerPage, currentPage);

            return Ok(books);
        }

        [Authorize(Roles = "Library User, Librarian")]
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

        [Authorize(Roles = "Librarian")]
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

        [Authorize(Roles = "Librarian")]
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
        
        [Authorize(Roles = "Librarian")]
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

        [Authorize(Roles = "Library User, Librarian")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchBookByQuery([FromQuery] int recordsPerPage, [FromQuery] int currentPage, [FromQuery] QueryObject query)
        {
            var books = await _bookService.SearchBookByQuery(query, recordsPerPage, currentPage);

            return Ok(books);
        }

        [Authorize(Roles = "Librarian")]
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

        [Authorize(Roles = "Library User")]
        [HttpPost("request")]
        public async Task<IActionResult> BookRequest([FromBody] BookRequestDto bookRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isRequested = await _bookService.BookRequest(bookRequest);

            if (!isRequested)
            {
                return BadRequest("Book request unsuceessful");
            }

            return Ok("Book request successful");
        }

        [Authorize(Roles = "Librarian, Library Manager")]
        [HttpPost("request/review")]
        public async Task<IActionResult> ReviewRequest([FromBody] ReviewRequestModel reviewRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccessful = await _bookService.ReviewRequest(reviewRequest);

            if (!isSuccessful)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}