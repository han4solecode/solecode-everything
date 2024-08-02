using OnlineFoodOrderingSystemWebAPI.Interfaces;
using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Services
{
    public class OrderService : IOrderService
    {
        private static List<Order> orders = [];
        private static int orderId = 300;

        public string? PlaceOrder(int customerId, List<Menu> menus, string? customerNote)
        {
            // get customer by id
            var customer = new CustomerService().GetCustomerById(customerId);

            // get all menu
            var availableMenu = new MenuService().GetAllMenu().FindAll(m => m.IsAvailable == true);

            // check if menu available to order
            var IsMenuAvailable = menus.All(x => availableMenu.Any(y => (y.Id == x.Id) && (y.Name == x.Name) && (y.Category == x.Category) && (y.Price == x.Price) && (y.Rating == x.Rating)));

            if (customer == null)
            {
                return null;
            }

            if (!IsMenuAvailable)
            {
                return null;
            }

            var orderNumber = Guid.NewGuid().ToString();
            
            var newOrder = new Order{
                Id = orderId + 1,
                CustomerId = customerId,
                Menus = menus,
                CustomerNote = customerNote,
                OrderNumber = orderNumber
            };

            orderId++;

            // calculate order total price
            newOrder.CalculatedTotalOrder();

            // add new order to orders
            orders.Add(newOrder);
            
            return orderNumber;
        }

        public Order? DisplayOrderDetails(string orderNumber)
        {
            var orderToDisplay = orders.FirstOrDefault(o => o.OrderNumber == orderNumber);

            if (orderToDisplay == null)
            {
                return null;
            }

            return orderToDisplay;
        }

        public bool CancelOrder(string orderNumber)
        {
            var orderToCancel = DisplayOrderDetails(orderNumber);

            if (orderToCancel == null)
            {
                return false;
            }

            orderToCancel.OrderStatus = "Canceled";
            return true;
        }

        public bool UpdateOrderStatus(string orderNumber, string inputStatus)
        {
            var orderToBeUpdated = DisplayOrderDetails(orderNumber);

            if (orderToBeUpdated == null)
            {
                return false;
            }

            orderToBeUpdated.OrderStatus = inputStatus;
            return true;
        }

        public string? GetOrderStatus(string orderNumber)
        {
            var order = DisplayOrderDetails(orderNumber);

            if (order == null)
            {
                return null;
            }

            return $"Order Status: {order.OrderStatus}";
        }


    }
}