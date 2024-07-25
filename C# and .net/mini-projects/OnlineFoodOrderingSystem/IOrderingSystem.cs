namespace OnlineFoodOrderingSystem
{
    public interface IOrderingSystem
    {
        // IOrderingSystem interface methods
        void AddRestaurant();
        void PlaceOrder();
        void DisplayOrderDetails();
        void CancelOrder();
        void GetOrderStatus();
    }
}