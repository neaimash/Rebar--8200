using Microsoft.AspNetCore.Mvc;
using Rebar.Models;
using Rebar.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Rebar.Model;

[ApiController]
[Route("api/[controller]")]
public class EndDayController : ControllerBase
{
    private readonly MongoDBContext _context;
    private readonly Account _account;

    public EndDayController(MongoDBContext context, Account account)
    {
        _context = context;
        _account = account;
    }

    [HttpPost("CloseCheckout")]
    public async Task<IActionResult> CloseCheckout([FromBody] string managerPassword)
    {
        string validManagerPassword = "6783395"; // Manager's password

        if (managerPassword != validManagerPassword)
        {
            return Unauthorized("Invalid manager password");
        }

        DateTime today = DateTime.Today;

        var ordersForToday = _account.Orders
            .Where(order => order.OrderDate.Date == today)
            .ToList();

        int totalOrdersToday = ordersForToday.Count;
        decimal totalSalesToday = ordersForToday.Sum(order => order.TotalPrice);

        Console.WriteLine($"Total orders today: {totalOrdersToday}");
        Console.WriteLine($"Total sales today: {totalSalesToday}");

        try
        {
            var endDayReport = new EndDayReport
            {
                date = today,
                SumOrders = totalOrdersToday,
                SumMoney = (int)totalSalesToday
            };

            await _context.EndDays.InsertOneAsync(endDayReport);

            return Ok("Checkout closed successfully. End-of-day report saved.");
        }
        catch (Exception ex)
        {
            return BadRequest("Failed to save the end-of-day report: " + ex.Message);
        }
    }
}
