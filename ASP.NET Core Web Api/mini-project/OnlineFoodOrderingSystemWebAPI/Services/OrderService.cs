using OnlineFoodOrderingSystemWebAPI.Interfaces;
using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Services
{
    public class OrderService : IOrderService
    {
        private static List<Order> orders = [];

        public string? PlaceOrder(int customerId, List<Menu> menus, string customerNote)
        {
            // get cutomer by id
            var customer = new CustomerService().GetCustomerById(customerId);

            // var cust = new Customer{
            //     CustomerId = customerId
            // };

            // get all menu
            var availableMenu = new MenuService().GetAllMenu();

            // check if menu available to order
            var IsMenuAvailable = availableMenu.Any(x => menus.Any(y => y.IsAvailable == true));

            
            if (customer == null)
            {
                return null;
            }

            if (!IsMenuAvailable)
            {
                return null;
            }

            var orderNumber = new Guid().ToString();
            
            var newOrder = new Order{
                // Id = 1,
                Id = orders.Last().Id + 1,
                CustomerId = customerId,
                // OrderDate = DateTime.Now,
                Menus = menus,
                CustomerNote = customerNote,
                OrderNumber = orderNumber,
            };

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