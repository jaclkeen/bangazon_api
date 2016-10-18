using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BangazonAPI.Controllers
{
    [ProducesAttribute("application/json")]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private BangazonContext context;
        public OrderController(BangazonContext ctx){
            context = ctx;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> orders = from order in context.Order select order;

            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Order order = context.Order.Single(m => m.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Order.Add(order);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetOrder", new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            order.OrderId = id;
            context.Order.Update(order);

            try{
                context.SaveChanges();
            }
            catch{
                throw;
            }

            return CreatedAtRoute("GetOrder", new { id = order.OrderId }, order);
        } 
        
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            foreach(Order order in context.Order){
                if(order.OrderId == id){
                    context.Order.Remove(order);
                    context.SaveChanges();
                }
            }
        }

        private bool OrderExists(int id)
        {
            return context.Order.Count(e => e.OrderId == id) > 0;
        }
    }
}