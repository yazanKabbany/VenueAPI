using System.Collections.Generic;
using VenuesApi.Data.Dto;
using VenuesApi.Data.Interfaces;
using VenuesApi.Models;
using System.Linq;
using System;

namespace VenuesApi.Data.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(VenuesDbContext context) : base(context)
        {
        }
        // Create a Customer or returns 0
        public int CreateCustomer(CustomerDto customerDto)
        {
            try
            {
                var customer = new Customer()
                {
                    Name = customerDto.Name,
                    Email = customerDto.Email
                };
                Context.Add(customer);
                Context.SaveChanges();
                return customer.id;
            }
            catch (Exception)//Something went wrong
            {
                return 0;
            }
        }
        //delete Customer with given id 
        public Status DeleteCustomer(int id)
        {
            var customer = Context.Customers.SingleOrDefault(v => v.id == id);
            if (customer == null)//no such customer
            {
                return Status.NotFound;
            }
            Context.Remove(customer);//delete customer
            Context.SaveChanges();
            return Status.Success;
        }
        // return customer with given id or null if no such Customer
        public CustomerDto GetCustomer(int id)
        {
            var customer = Context.Customers.SingleOrDefault(v => v.id == id);
            if (customer == null)//no such customer
            {
                return null;
            }
            var customerDto = new CustomerDto()
            {
                id = customer.id,
                Name = customer.Name,
                Email = customer.Email
            };
            return customerDto;
        }

        //Get all customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customerDtoList = Context.Customers.Select(customer =>
            new CustomerDto()
            {
                id = customer.id,
                Name = customer.Name,
                Email = customer.Email
            }).ToList();
            return customerDtoList;
        }
        //update customer
        public Status UpdateCustomer(int id, CustomerDto customerDto)
        {
            var customer = Context.Customers.SingleOrDefault(v => v.id == id);
            if (customer == null)//No such Customer
            {
                return Status.NotFound;
            }
            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            Context.Update(customer);
            Context.SaveChanges();
            return Status.Success;
        }
    }
}