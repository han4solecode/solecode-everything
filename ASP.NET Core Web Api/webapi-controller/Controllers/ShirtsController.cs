using Microsoft.AspNetCore.Mvc;
using webapi_controller.Models;

namespace webapi_controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Get all shirts";
        }

        [HttpGet("{id}")]
        public string GetByID(int id)
        {
            return $"Get shirt with ID: {id}";
        }

        [HttpPost]
        public string Post([FromBody]Shirt shirt)
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