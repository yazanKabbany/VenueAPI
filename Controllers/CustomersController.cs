using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VenuesApi.Data;
using VenuesApi.Data.Dto;
using VenuesApi.Data.Interfaces;


namespace VenuesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public ICustomerRepository CustomerRepository { get; }
        public CustomersController(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository;
        }
        // get all customers
        [HttpGet]
        [ProducesResponseType(200,
        Type = typeof(IEnumerable<CustomerDto>))]
        public IActionResult GetCustomers()
        {
            return Ok(CustomerRepository.GetCustomers());
        }

        //get customer with given id
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        public IActionResult GetCustomer(int id)
        {
            var customerDto = CustomerRepository.GetCustomer(id);
            if(customerDto == null)
            {
                return NotFound();
            }
            return Ok(customerDto);
        }
        //create new customer
        [HttpPost]
        public IActionResult PostCustomer(CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var id = CustomerRepository.CreateCustomer(customerDto);
            if(id == 0)
            {
                return BadRequest();
            }
            customerDto.id = id;

            return CreatedAtAction(nameof(GetCustomer), new {id = id}, customerDto);            
        }
        //Update customer
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var status = CustomerRepository.UpdateCustomer(id, customerDto);
            if(status == Status.Error)
            {
                return BadRequest();
            }
            if(status == Status.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var status = CustomerRepository.DeleteCustomer(id);
            if(status == Status.Error)
            {
                return BadRequest();
            }
            if(status == Status.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }
        
    }
}