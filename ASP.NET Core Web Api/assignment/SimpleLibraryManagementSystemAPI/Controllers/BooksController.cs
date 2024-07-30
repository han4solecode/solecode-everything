using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleLibraryManagementSystemAPI.Models;
using SimpleLibraryManagementSystemAPI.Services;

namespace SimpleLibraryManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _bookService = new();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookService.GetAllBooks());
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var book = _bookService.GetBookById(id);
            
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            _bookService.CreateBook(book);

            return Created($"books/{book.ID}", book);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book inputBook)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var book = _bookService.UpdateBook(id, inputBook);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var isDeleted = _bookService.DeleteBook(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}