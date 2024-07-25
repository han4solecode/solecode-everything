namespace OnlineFoodOrderingSystem
{
    public interface IOrderingSystem
    {
        // IOrderingSystem interface methods
        void AddRestaurant(Restaurant restaurant);
        string PlaceOrder(string restaurantName, List<MenuItem> orderedItems);
        void DisplayOrderDetails(string orderNumber);
        void CancelOrder();
        void GetOrderStatus();
    }
}