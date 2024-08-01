using Microsoft.AspNetCore.Mvc;
using OnlineFoodOrderingSystemWebAPI.Interfaces;
using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            var orderNumber = _orderService.PlaceOrder(order.CustomerId, order.Menus, order.CustomerNote);

            if (orderNumber == null)
            {
                return BadRequest();
            }

            return Ok(orderNumber);
        }

        [HttpGet]
        public IActionResult DisplayOrderDetails([FromQuery] string orderNumber)
        {
            var order = _orderService.DisplayOrderDetails(orderNumber);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet]
        [Route("status")]
        public IActionResult GetOrderStatus([FromQuery] string orderNumber)
        {
            var orderStatus = _orderService.GetOrderStatus(orderNumber);

            if (orderStatus == null)
            {
                return NotFound();
            }

            return Ok(orderStatus);
        }

        [HttpPatch]
        [Route("status")]
        public IActionResult UpdateOrderStatus([FromQuery] string orderNumber, [FromQuery] string newStatus)
        {
            var isUpdated = _orderService.UpdateOrderStatus(orderNumber, newStatus);

            if (!isUpdated)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("cancel")]
        public IActionResult CancelOrder([FromQuery] string orderNumber)
        {
            var isCanceled = _orderService.CancelOrder(orderNumber);

            if (!isCanceled)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}