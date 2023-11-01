using Microsoft.AspNetCore.Mvc;
using Rebar.Model;
using Rebar.Models;
using Rebar.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Rebar.ReqestsModels;
using static Rebar.Model.MenuShake;

namespace Rebar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuManagementController : ControllerBase
    {
        private readonly MongoDBContext _context;
        private readonly Menu _menu;
        private readonly Account _account; 


        public MenuManagementController(MongoDBContext context, Menu menu, Account account)
        {
            _context = context;
            _menu = menu;
            _account = account;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAndSaveOrder([FromBody] CreateOrderReqest request)
        {
            if (request == null || request.Shakes == null || request.Shakes.Count == 0 || string.IsNullOrEmpty(request.ClientName))
            {
                return BadRequest("Incomplete order details.");
            }

            if (request.Shakes.Count > 10)
            {
                return BadRequest("Exceeded maximum order limit (10 shakes per order).");
            }

            var newOrder = new Order
            {
                OrderCreationTime = DateTime.Now,
                CustomerName = request.ClientName,
                OrderDate = DateTime.Today,
                OrderPreparationEndTime = DateTime.Now,
                OrderID = Guid.NewGuid()
            };

            var orderShakes = new List<OrderShake>();
            decimal totalPrice = 0;

            foreach (var createShake in request.Shakes)
            {
                var shake = _menu.GetShakeByName(createShake.Name);

                if (shake != null)
                {
                    if (Enum.TryParse<ShakeSize>(createShake.ShakeSize, out ShakeSize size))
                    {
                        orderShakes.Add(new OrderShake(shake, size));
                        totalPrice += shake.GetPrice(size);
                    }
                    else
                    {
                        return BadRequest("Invalid ShakeSize.");
                    }
                }
                else
                {
                    return BadRequest("Invalid Shake.");
                }
            }

            if (totalPrice <= 0)
            {
                return BadRequest("Invalid order, no valid shakes found.");
            }

            newOrder.TotalPrice = totalPrice;
            newOrder.Shakes = orderShakes;

            try
            {
                await _context.Orders.InsertOneAsync(newOrder);
                // Use the Account service to add the order
                _account.AddOrderToAcount(newOrder);

                return Ok("Order saved successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to save the order: " + ex.Message);
            }
        }
    }
}
