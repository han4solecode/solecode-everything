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

        /// <summary>
        /// Place a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns>This endpoint returns an order number of the newly placed order</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/order
        ///     {
        ///         "customerId": 1,
        ///         "customerNote": "less sugar",
        ///         "menus": [
        ///             {
        ///                 "id": 1,
        ///                 "name": "Gurame Goreng",
        ///                 "price": 20000,
        ///                 "category": "Food",
        ///                 "rating": 4.5
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "name": "Es Teh",
        ///                 "price": 7000,
        ///                 "category": "Beverage",
        ///                 "rating": 5
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns an order number of the newly placed order</response>
        /// <response code="400">Something is wrong :(</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            var orderNumber = _orderService.PlaceOrder(order.CustomerId, order.Menus, order.CustomerNote);

            if (orderNumber == null)
            {
                return BadRequest("Something is wrong");
            }

            return Ok(orderNumber);
        }

        /// <summary>
        /// Get a specific order data
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns>This endpoint returns an order data</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/order?orderNumber={orderNumber}
        /// 
        /// </remarks>
        /// <response code="200">Returns an order data</response>
        /// <response code="404">If the order does not exist</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DisplayOrderDetails([FromQuery] string orderNumber)
        {
            var order = _orderService.DisplayOrderDetails(orderNumber);

            if (order == null)
            {
                return NotFound("Order does not exist");
            }

            return Ok(order);
        }

        /// <summary>
        /// Get a specific order status
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns>This endpoint returns an order status</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/order/status?orderNumber={orderNumber}
        /// 
        /// </remarks>
        /// <response code="200">Returns an order status</response>
        /// <response code="404">If the order does not exist</response>
        [HttpGet]
        [Route("status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrderStatus([FromQuery] string orderNumber)
        {
            var orderStatus = _orderService.GetOrderStatus(orderNumber);

            if (orderStatus == null)
            {
                return NotFound("Order does not exist");
            }

            return Ok(orderStatus);
        }

        /// <summary>
        /// Updates a specific order status
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="newStatus"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH api/order/status?orderNumber={orderNumber}&amp;newStatus={newStatus}
        /// 
        /// </remarks>
        /// <response code="204">Succesful update</response>
        /// <response code="404">If the order does not exist</response>
        [HttpPatch]
        [Route("status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateOrderStatus([FromQuery] string orderNumber, [FromQuery] string newStatus)
        {
            var isUpdated = _orderService.UpdateOrderStatus(orderNumber, newStatus);

            if (!isUpdated)
            {
                return BadRequest("Order does not exist");
            }

            return NoContent();
        }

        /// <summary>
        /// Cancels an order
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns>No Content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/order/cancel?orderNumber={orderNumber}
        /// 
        /// </remarks>
        /// <response code="204">Successful cancel</response>
        /// <response code="404">If the order does not exist</response>
        [HttpPost]
        [Route("cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CancelOrder([FromQuery] string orderNumber)
        {
            var isCanceled = _orderService.CancelOrder(orderNumber);

            if (!isCanceled)
            {
                return BadRequest("Order does not exist");
            }

            return NoContent();
        }
    }
}