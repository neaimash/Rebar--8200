using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rebar.Model;
using Rebar.Models;
using Rebar.Data;
using System.Linq;
namespace Rebar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaveOrderController : ControllerBase
    {
        private readonly MongoDBContext _context;

        public SaveOrderController(MongoDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            if (order == null)
            {
                return BadRequest("Invalid order details.");
            }

            try
            {
                await _context.Orders.InsertOneAsync(order);
                return CreatedAtRoute(new { id = order.OrderID }, order); // Created response
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to save the order: " + ex.Message); // Handle server error
            }
        }
       
    }
}
