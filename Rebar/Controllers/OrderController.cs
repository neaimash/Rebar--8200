using Microsoft.AspNetCore.Mvc;
using Rebar.Model;
using Rebar.Models;
using Rebar.ReqestsModels;

namespace Rebar.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class OrderController : ControllerBase
    {
        private Menu menu; 

        public OrderController()
        {
            menu = new Menu(); // Initialize the menu
            menu.AddShake("Vanilla Shake", "Classic vanilla flavor", 22, 25, 30);
            menu.AddShake("Shoko Shake", "Classic shoko flavor", 21, 24, 29);
        }
       
        /* public string Index()
         {
             return "hii";
         }
        */
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderReqest request)
        {
            if (request == null || request.Shakes == null || request.Shakes.Count == 0 || string.IsNullOrEmpty(request.ClientName))
            {
                return BadRequest("Incomplete order details.");
            }

            if (request.Shakes.Count > 10)
            {
                return BadRequest("Exceeded maximum order limit (10 shakes per order).");
            }

            // Record the time when the order is received
            DateTime orderReceivedTime = DateTime.Now;

            // Fetch shakes from the menu based on the order request
            var orderShakes = new List<OrderShake>();
            decimal totalPrice = 0; // Total price for the order

            foreach (var createShake in request.Shakes)
            {
                var shake = menu.GetShakeByID(createShake.ID); // Get shake from the menu by ID
                if (shake != null)
                {
                    orderShakes.Add(new OrderShake(shake, createShake.ShakeSize));
                    totalPrice += shake.GetPrice(createShake.ShakeSize); // Calculate the price for the shake
                }
            }

            if (totalPrice <= 0)
            {
                return BadRequest("Invalid order, no valid shakes found.");
            }

            // Create the order
            var newOrder = new Order(orderShakes, request.ClientName, null); // You may add discounts here

            // Generate a unique ID for the order
            newOrder.OrderID = Guid.NewGuid();

            // Set the order date as the current date and time
            newOrder.OrderDate = DateTime.Now;

            // Set the total price for the order
            newOrder.TotalPrice = totalPrice;
            // Record the time when the order is processed
            DateTime orderProcessedTime = DateTime.Now;

            // Calculate the time taken to process the order
            TimeSpan orderProcessingTime = orderProcessedTime - orderReceivedTime;

            return Ok(newOrder); // Return the created order or save it in the database
        }

    }




}
