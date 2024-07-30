using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Get all todo items";
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (id > 5)
            {
                return NotFound();
            }

            return Ok($"Get todo item with ID: {id}");
        }

        [HttpPost]
        public string Post([FromBody] string todo)
        {
            return $"Create a new todo: {todo}";
        }

        [Route("create")]
        [HttpPost]
        public string Create([FromBody] string todo)
        {
            return $"Create a new todo: {todo}";
        }

        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string inputTodo)
        {
            return $"Update todo with ID: {id} with: {inputTodo}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Todo with ID {id} has been deleted";
        }


        // for product stuff
        [Route("product")]
        [HttpPost]
        public string CreateProduct([FromBody] Product product)
        {
            return $"Product {product.Name} with ${product.Price} price tag has been created";
        }
    }
}
