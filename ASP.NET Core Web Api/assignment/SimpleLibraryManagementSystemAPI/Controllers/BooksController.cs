using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleLibraryManagementSystemAPI.Models;

namespace SimpleLibraryManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Get all books");
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            return Ok($"Get a book with ID: {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            return Created($"books/{book.ID}", book);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Book inputBook)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            return Ok($"Book by ID {id} has been updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            return Ok($"Book by ID {id} has been deleted");
        }

    }
}