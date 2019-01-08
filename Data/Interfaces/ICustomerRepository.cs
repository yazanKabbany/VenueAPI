
using System.Collections.Generic;
using VenuesApi.Data.Dto;

namespace VenuesApi.Data.Interfaces
{
    public interface ICustomerRepository
    {
        CustomerDto GetCustomer(int id);
        IEnumerable<CustomerDto> GetCustomers();
        Status DeleteCustomer(int id);
        int CreateCustomer(CustomerDto customerDto);
        Status UpdateCustomer(int id, CustomerDto customerDto);
    }
}
