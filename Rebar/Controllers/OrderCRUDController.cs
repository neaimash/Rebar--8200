using Microsoft.AspNetCore.Mvc;
using Rebar.Data;
using Rebar.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rebar.Data;
using Rebar.Model;

[ApiController]
[Route("api/[controller]")]
public class OrderCRUDController : ControllerBase
{
    private readonly MongoDBContext _context;

    public OrderCRUDController(MongoDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> Get()
    {
        return await _context.Orders.Find(_ => true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> Get(string id)
    {
        // Parse the ID string to Guid for comparison
        if (!Guid.TryParse(id, out Guid parsedId))
        {
            return BadRequest("Invalid ID format");
        }

        var order = await _context.Orders.Find(p => p.OrderID == parsedId).FirstOrDefaultAsync();

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Order orderIn)
    {
        if (!Guid.TryParse(id, out Guid parsedId))
        {
            return BadRequest("Invalid ID format");
        }

        var order = await _context.Orders.Find(p => p.OrderID == parsedId).FirstOrDefaultAsync();

        if (order == null)
        {
            return NotFound();
        }

        // Update the shake document
        await _context.Orders.ReplaceOneAsync(p => p.OrderID == parsedId, orderIn);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (!Guid.TryParse(id, out Guid parsedId))
        {
            return BadRequest("Invalid ID format");
        }

        var order = await _context.Orders.Find(p => p.OrderID == parsedId).FirstOrDefaultAsync();

        if (order == null)
        {
            return NotFound();
        }

        // Delete the shake document
        await _context.Orders.DeleteOneAsync(p => p.OrderID == parsedId);

        return NoContent();
    }
}