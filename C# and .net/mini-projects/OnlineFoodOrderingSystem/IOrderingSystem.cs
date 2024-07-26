namespace OnlineFoodOrderingSystem
{
    // Interface is used to make a "contract" to its concrete class
    // At the concrete class, we have to implement all of the method of the interface
    // Implemented method in each concrete class may be different, just keep in mind the method name and parameters
    public interface IOrderingSystem
    {
        // IOrderingSystem interface methods
        void AddRestaurant(Restaurant restaurant);
        string PlaceOrder(string restaurantName, List<MenuItem> orderedItems);
        void DisplayOrderDetails(string orderNumber);
        void CancelOrder(string orderNumber);
        void GetOrderStatus(string orderNumber);
    }
}