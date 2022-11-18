using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiNorthwind.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApiNorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly NorthwindContext _context;

        public CustomerController(NorthwindContext context)
        {
            _context = context;
        }


        // GET /api/customer
        [HttpGet]
        public List<Customer> Get()
        {
            List<Customer> customers = (from c in _context.Customers
                                        select c).ToList();
            return customers;
        }


        // GET /api/customer/id
        [HttpGet("{id}")]
        public Customer Get(string id)
        {
            var customer = (from c in _context.Customers
                            where c.CustomerId == id
                            select c).SingleOrDefault();
            return customer;
        }


        // GET /api/customer/companyName/contactName
        [HttpGet("{companyName}/{contactName}")]
        public dynamic Get(string companyName, string contactName)
        {
            dynamic customer = (from c in _context.Customers
                                where c.CompanyName == companyName && c.ContactName == contactName
                                select new { c.CompanyName, c.ContactName, c.ContactTitle, c.Phone });
            return customer;
        }


        // POST /api/customer
        [HttpPost]
        public ActionResult Post(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok();
        }


        // PUT /api/customer
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }


        // DELETE /api/customer
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            Customer customer = (from c in _context.Customers
                                 where c.CustomerId == id
                                 select c).SingleOrDefault();
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
