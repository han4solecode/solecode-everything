using System.Collections;

namespace OnlineFoodOrderingSystem
{
    
    public class OnlineFoodOrderingSystem : IOrderingSystem
    {
        private List<Restaurant> restaurantList = [];
        private Dictionary<string, Order> placedOrderInRestaurant = [];
        private string? orderNumber;

        // props
        public List<Restaurant> RestaurantList
        {
            get => restaurantList;
        }
        public Dictionary<string, Order> PlacedOrderInRestaurant
        {
            get => placedOrderInRestaurant;
        }

        // IOrderingSystem methods implementation
        // method for adding Restaurant to the system
        public void AddRestaurant(Restaurant restaurant)
        {
            restaurantList.Add(restaurant);
            Console.WriteLine("{0} has been added to the system successfully", restaurant.Name);
        }

        // method to place order in a specific Restaurant
        public string PlaceOrder(string restaurantName, List<MenuItem> orderedItems)
        {
            // get restaurant from restaurantList where Name == restaurantName
            var restoToPlaceOrder = restaurantList.SingleOrDefault(r => r.Name == restaurantName);

            // check if restaurant exist
            if (restoToPlaceOrder != null)
            {
                // create order number with Guid 
                orderNumber = Guid.NewGuid().ToString();

                // create new Order object to be inserted to Restaurant
                Order newOrder = new(orderedItems, "placed");

                // calculate total order cost
                newOrder.CalculateTotal();

                // insert new order with ReceiveOrder method
                restoToPlaceOrder.ReceiveOrders(newOrder);
                Console.WriteLine("Your order has been placed at {0}", restoToPlaceOrder.Name);

                // insert order number and its order to a dictionary
                placedOrderInRestaurant.Add(orderNumber, newOrder);

                // return the order number
                return orderNumber;

            }
            else
            {
                return restaurantName + " is does not exist";
            }

        }

        // method for displaying order display through order number
        public void DisplayOrderDetails(string orderNumber)
        {
            // check if order number exist
            if (placedOrderInRestaurant.TryGetValue(orderNumber, out Order? order))
            {
                Console.WriteLine("\n==== Ordered Items ====");
                Console.WriteLine("Order Number: {0}", orderNumber);
                Console.WriteLine("Order Status: {0}", order.Status);
                Console.WriteLine("=======================");

                foreach (MenuItem item in order.MenuItemList)
                {
                    // use MenuInfo() method
                    Console.WriteLine(item.MenuInfo());
                    Console.WriteLine("-----------------------");
                }

                Console.WriteLine("Total cost: {0}\n", order.TotalPrice);
            }
            else
            {
                Console.WriteLine("Order with {0} order number is does not exist", orderNumber);
            }
        }

        // method for canceling placed order
        public void CancelOrder(string orderNumber)
        {
            if (placedOrderInRestaurant.TryGetValue(orderNumber, out Order? order))
            {
                order.Status = "canceled";
                Console.WriteLine("Order with {0} order number is canceled", orderNumber);
            }
            else
            {
                Console.WriteLine("Order with {0} order number is does not exist", orderNumber);
            }
        }


        public void GetOrderStatus(string orderNumber)
        {
            if (placedOrderInRestaurant.TryGetValue(orderNumber, out Order? order))
            {
                Console.WriteLine("{0} order status: {1}", orderNumber, order.Status);
            }
            else
            {
                Console.WriteLine("Order with {0} order number is does not exist", orderNumber);
            }
        }

    }
}