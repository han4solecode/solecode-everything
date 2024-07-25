using System.Collections;

namespace OnlineFoodOrderingSystem
{
    public class OnlineFoodOrderingSystem : IOrderingSystem
    {
        private List<Restaurant> restaurantList = [];
        private Dictionary<string, Order> placedOrderInRestaurant = [];
        private List<Order> savedOrderList = [];
        private int totalOrderCost;
        private string? orderNumber;

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
                // calculate total order cost
                foreach (MenuItem item in orderedItems)
                {
                    totalOrderCost += item.CalucatePrice();
                }

                // create new Order object to be inserted to Restaurant
                Order newOrder = new(orderedItems, totalOrderCost);

                // insert new order with ReceiveOrder method
                restoToPlaceOrder.ReceiveOrders(newOrder);
                Console.WriteLine("Your order has been placed at {0}", restoToPlaceOrder.Name);

                savedOrderList.Add(newOrder);
                
                // create order number with Guid 
                orderNumber = Guid.NewGuid().ToString();
                
                // insert order number and its resto to a dictionary
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
            if (placedOrderInRestaurant.ContainsKey(orderNumber))
            {
                Order themOrderInResto = placedOrderInRestaurant[orderNumber];
                var orderCost = 0;

                // var realOrder = savedOrderList.Find(o => o.MenuItemList == themOrderInResto.MenuItemList);

                Console.WriteLine("\n==== Ordered Items ====");

                foreach (MenuItem item in themOrderInResto.MenuItemList)
                {
                    // use MenuInfo() method
                    Console.WriteLine(item.MenuInfo());
                    Console.WriteLine("-----------------------");

                    // calculate price using CalculatePrice() method
                    orderCost += item.CalucatePrice();
                }

                Console.WriteLine("Total cost: {0}", orderCost);
            }
            else
            {
                Console.WriteLine("Order with {0} order number is does not exist", orderNumber);
            }
        }

        public void CancelOrder(){}
        public void GetOrderStatus(){}

    }
}