using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BangazonAPI.Controllers
{
    // $env:Bangazon_Db_Path="C:\Users\Jacob\workspace\backend\bangazon\
    // BangazonAPI\bangazon.db"
    
    [ProducesAttribute("application/json")]
    // LOCALHOST:5000/CUSTOMER
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private BangazonContext context;
        public CustomersController(BangazonContext ctx){
            context = ctx;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> customers = from customer in context.Customer select customer;

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer customer = context.Customer.Single(m => m.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }
                
                return Ok(customer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
            public IActionResult Post([FromBody] Customer customer)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                context.Customer.Add(customer);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (CustomerExists(customer.CustomerId))
                    {
                        return new StatusCodeResult(StatusCodes.Status409Conflict);
                    }
                    else
                    {
                        throw;
                    }
                }
            return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customer.CustomerId = id;
            context.Customer.Update(customer);

            try{
                context.SaveChanges();
            }
            catch{
                throw;
            }

            return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, customer);
        } 
        
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            foreach(Customer cust in context.Customer){
                if(cust.CustomerId == id){
                    context.Customer.Remove(cust);
                    context.SaveChanges();
                }
            }
        }
        private bool CustomerExists(int id)
        {
            return context.Customer.Count(e => e.CustomerId == id) > 0;
        }
    }
}

