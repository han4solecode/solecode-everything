using Microsoft.AspNetCore.Mvc;
using SimpleLibraryManagementSystemAPI_PosgreSQL.Interfaces;
using SimpleLibraryManagementSystemAPI_PosgreSQL.Models;

namespace SimpleLibraryManagementSystemAPI_PosgreSQL.Controllers
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
        public IActionResult Get()
        {
            return Ok(_bookRepository.GetAllBooks());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var book = _bookRepository.GetBookbyId(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            var newBook = _bookRepository.CreateBook(book);

            if (newBook == null)
            {
                return BadRequest();
            }

            return Created($"book/{newBook.Id}", newBook);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book inputBook)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var updatedBook = _bookRepository.UpdateBook(id, inputBook);

            if (updatedBook == null)
            {
                return NotFound();
            }

            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var isBookDeleted = _bookRepository.DeleteBook(id);

            if (!isBookDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}