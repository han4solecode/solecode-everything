using Microsoft.AspNetCore.Mvc;
using OnlineFoodOrderingSystemWebAPI.Interfaces;
using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            _customerService.AddCustomer(customer);
            return Created($"customer/{customer.CustomerId}", customer);
        }

        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            return Ok(_customerService.GetAllCustomer());
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var customer = _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer inputCustomer)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var customer = _customerService.UpdateCustomer(id, inputCustomer);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var isDeleted = _customerService.DeleteCustomer(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}