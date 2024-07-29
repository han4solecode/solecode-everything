using Microsoft.AspNetCore.Mvc;
using webapi_controller.Models;

namespace webapi_controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShirtsController : ControllerBase
    {
        private readonly List<Shirt> shirtList =
        [
            new Shirt {ID = 1, Brand = "Brand 1", Color = "Black", Gender = "male", Price = 20, Size = 8},
            new Shirt {ID = 2, Brand = "Brand 2", Color = "Yellow", Gender = "male", Price = 20, Size = 10},
            new Shirt {ID = 3, Brand = "Brand 3", Color = "Pink", Gender = "female", Price = 20, Size = 6},
            new Shirt {ID = 4, Brand = "Brand 4", Color = "Purle", Gender = "female", Price = 20, Size = 7}
        ];

        [HttpGet]
        public string Get()
        {
            return "Get all shirts";
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var shirt = shirtList.FirstOrDefault(s => s.ID == id);
            if (shirt == null)
            {
                return NotFound();
            }

            return Ok(shirt);
        }

        [HttpPost]
        public string Post([FromBody] Shirt shirt)
        {
            return $"New {shirt.Brand} shirt is created with {shirt.Color} color, size {shirt.Size}, for {shirt.Gender}, and a ${shirt.Price} price tag";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Update shirt with ID: {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Delete shirt with ID: {id}";
        }
    }
}