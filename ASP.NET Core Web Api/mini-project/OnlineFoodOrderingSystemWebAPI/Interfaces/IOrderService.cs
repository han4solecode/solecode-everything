using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Interfaces
{
    public interface IOrderService
    {
        string PlaceOrder(int customerId, List<Menu> menus);

        Order DisplayOrderDetails(string orderNumber);

        void CancelOrder(string orderNumber);

        void UpdateOrderStatus(string orderNumber);

        string GetOrderStatus(string orderNumber);
    }
}