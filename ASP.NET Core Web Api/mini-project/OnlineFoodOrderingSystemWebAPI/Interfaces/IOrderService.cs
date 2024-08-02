using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Interfaces
{
    public interface IOrderService
    {
        string? PlaceOrder(int customerId, List<Menu> menus, string? customerNote);

        Order? DisplayOrderDetails(string orderNumber);

        bool CancelOrder(string orderNumber);

        bool UpdateOrderStatus(string orderNumber, string inputStatus);

        string? GetOrderStatus(string orderNumber);
    }
}