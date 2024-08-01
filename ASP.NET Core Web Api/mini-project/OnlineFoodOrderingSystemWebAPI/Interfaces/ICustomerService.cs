using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Interfaces
{
    public interface ICustomerService
    {
        Customer AddCustomer(Customer customer);

        List<Customer> GetAllCustomer();

        Customer? GetCustomerById(int id);

        Customer? UpdateCustomer(int id, Customer inputCustomer);

        bool DeleteCustomer(int id);
    }
}