using OnlineFoodOrderingSystemWebAPI.Interfaces;
using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private static List<Customer> customers = [];

        public CustomerService()
        {
            
        }

        public Customer AddCustomer(Customer customer)
        {
            customers.Add(customer);
            return customer;
        }

        public List<Customer> GetAllCustomer()
        {
            return customers;
        }

        public Customer? GetCustomerById(int id)
        {
            var customerById = customers.FirstOrDefault(c => c.CustomerId == id);

            if (customerById == null)
            {
                return null;
            }

            return customerById;
        }

        public Customer? UpdateCustomer(int id, Customer inputCustomer)
        {
            var customerToBeUpdated = GetCustomerById(id);

            if (customerToBeUpdated == null)
            {
                return null;
            }

            customerToBeUpdated.CustomerId = inputCustomer.CustomerId;
            customerToBeUpdated.Name = inputCustomer.Name;
            customerToBeUpdated.Email = inputCustomer.Email;
            customerToBeUpdated.PhoneNumber = inputCustomer.PhoneNumber;
            customerToBeUpdated.Address = inputCustomer.Address;
            customerToBeUpdated.RegistrationDate = inputCustomer.RegistrationDate;

            return customerToBeUpdated;
        }

        public bool DeleteCustomer(int id)
        {
            var customerToBeDeleted = GetCustomerById(id);

            if (customerToBeDeleted == null)
            {
                return false;
            }

            customers.Remove(customerToBeDeleted);
            return true;
        }
    }
}