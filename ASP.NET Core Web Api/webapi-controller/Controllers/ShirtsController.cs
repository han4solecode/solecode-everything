using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using webapi_controller.Models;

namespace webapi_controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShirtsController : ControllerBase
    {
        private List<Shirt> shirtList =
        [
            new Shirt {ID = 1, Brand = "Brand 1", Color = "Black", Gender = "male", Price = 20, Size = 8},
            new Shirt {ID = 2, Brand = "Brand 2", Color = "Yellow", Gender = "male", Price = 20, Size = 10},
            new Shirt {ID = 3, Brand = "Brand 3", Color = "Pink", Gender = "female", Price = 20, Size = 6},
            new Shirt {ID = 4, Brand = "Brand 4", Color = "Purle", Gender = "female", Price = 20, Size = 7}
        ];

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(shirtList);
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
        public IActionResult Post([FromBody] Shirt shirt)
        {
            var newShirt = new Shirt {ID = shirt.ID, Brand = shirt.Brand, Color = shirt.Color, Gender = shirt.Gender, Price = shirt.Price, Size = shirt.Size};

            shirtList.Add(newShirt);

            return Created($"/shirts/{shirt.ID}", shirt);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Shirt inputShirt)
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

            shirt.Brand = inputShirt.Brand;
            shirt.Color = inputShirt.Color;
            shirt.Gender = inputShirt.Gender;
            shirt.Price = inputShirt.Price;
            shirt.Size = inputShirt.Size;

            return Ok(shirt);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if ( id <= 0)
            {
                return BadRequest();
            }

            var shirt = shirtList.FirstOrDefault(s => s.ID == id);

            if (shirt == null)
            {
                return NotFound();
            }

            shirtList.Remove(shirt);

            return Ok($"Shirt with ID {id} has been removed");
        }
    }
}