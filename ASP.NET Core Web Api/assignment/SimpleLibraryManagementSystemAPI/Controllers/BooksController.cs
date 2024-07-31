using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleLibraryManagementSystemAPI.Interfaces;
using SimpleLibraryManagementSystemAPI.Models;
using SimpleLibraryManagementSystemAPI.Services;

namespace SimpleLibraryManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        /// <summary>
        /// Retrieve all available Books
        /// </summary>
        /// <returns>A list of Books</returns>
        /// <response code="200">Returns a list Books</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_booksService.GetAllBooks());
        }

        /// <summary>
        /// Retrieve a specific Book
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Book</returns>
        /// <response code="200">Returns a Book</response>
        /// <response code="400">If id is less than or equals to 0</response>
        /// <response code="404">If the Book does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var book = _booksService.GetBookById(id);
            
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        
        /// <summary>
        /// Creates a new Book
        /// </summary>
        /// <param name="book"></param>
        /// <returns>A newly created Book</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/v1/books
        ///     {
        ///         "id": 1,
        ///         "title": "Clean Coder",
        ///         "author": "Robert Cecil Martin",
        ///         "publicationYear": 2011,
        ///         "isbn": "978-0137081073"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created Book</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Book book)
        {
            _booksService.CreateBook(book);

            return Created($"books/{book.ID}", book);
        }

        /// <summary>
        /// Updates an existing Book 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputBook"></param>
        /// <returns>A newly updated Book</returns>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/v1/books/1
        ///     {
        ///         "id": 1,
        ///         "title": "Clean Coder",
        ///         "author": "Uncle Bob",
        ///         "publicationYear": 2011,
        ///         "isbn": "978-0137081073"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly updated Book</response>
        /// <response code="400">If id is less than or equals to 0</response>
        /// <response code="400">If the input Book is null</response>
        /// <response code="404">If the Book does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Book inputBook)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var book = _booksService.UpdateBook(id, inputBook);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        /// <summary>
        /// Deletes a specific Book
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No Content</returns>
        /// <response code="204">Delete book is successfull</response>
        /// <response code="400">If id is less than or equals to 0</response>
        /// <response code="404">If the Book does not exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var isDeleted = _booksService.DeleteBook(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}