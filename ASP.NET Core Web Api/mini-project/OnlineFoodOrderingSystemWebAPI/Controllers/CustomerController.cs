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

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>This endpoint returns a newly created customer</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/customer
        ///     {
        ///         "customerId": 1,
        ///         "name": "Shay Eyden",
        ///         "email": "seyden0@elpais.com",
        ///         "phoneNumber": "744-719-8848",
        ///         "address": "873 Muir Crossing"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">Returns the newly created customer</response>
        /// <response code="400">If customer already exist</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            var existingCustomer = _customerService.GetCustomerById(customer.CustomerId);

            if (existingCustomer != null)
            {
                return BadRequest("Customer with this id already exist");
            }

            _customerService.AddCustomer(customer);
            return Created($"customer/{customer.CustomerId}", customer);
        }

        /// <summary>
        /// Get all customer data
        /// </summary>
        /// <returns>This endpoint returns a list of customer</returns>
        /// <response code="200">Return a list of customer</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllCustomer()
        {
            return Ok(_customerService.GetAllCustomer());
        }

        /// <summary>
        /// Get a specific customer data
        /// </summary>
        /// <param name="id"></param>
        /// <returns>This endpoint returns a customer data</returns>
        /// <response code="200">Returns a menu</response>
        /// <response code="400">If id is less than or equal to 0</response>
        /// <response code="204">If the customer does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id input");
            }

            var customer = _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound("Customer does not exist");
            }

            return Ok(customer);
        }
        
        /// <summary>
        /// Updates an existing customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputCustomer"></param>
        /// <returns>This endpoint returns a newly updated customer</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/customer
        ///     {
        ///         "customerId": 1,
        ///         "name": "Shay Eyden",
        ///         "email": "seyden0@elpais.com",
        ///         "phoneNumber": "744-719-2211",
        ///         "address": "873 Muir Crossing"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Returns the newly updated customer</response>
        /// <response code="400">If id is less than or equal to 0</response>
        /// <response code="404">If customer does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer inputCustomer)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id input");
            }

            var customer = _customerService.UpdateCustomer(id, inputCustomer);

            if (customer == null)
            {
                return NotFound("Customer does not exist");
            }

            return Ok(customer);
        }

        /// <summary>
        /// Deletes a specific customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <response code="204">Successful customer delete</response>
        /// <response code="400">If id is less than or equal to 0</response>
        /// <response code="404">If menu does not exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id input");
            }

            var isDeleted = _customerService.DeleteCustomer(id);

            if (!isDeleted)
            {
                return NotFound("Customer does not exist");
            }

            return NoContent();
        }
    }
}