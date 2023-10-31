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
        private readonly HttpClient _httpClient;
        public OrderController(HttpClient httpClient)
        {
           
            menu = new Menu(); // Initialize the menu
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/Orders");


            // menu.AddShake("Vanilla Shake", "Classic vanilla flavor", 22, 25, 30);
            // menu.AddShake("Shoko Shake", "Classic shoko flavor", 21, 24, 29);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderReqest request)
        {
            if (request == null || request.Shakes == null || request.Shakes.Count == 0 || string.IsNullOrEmpty(request.ClientName))
            {
                return BadRequest("Incomplete order details.");
            }

            if (request.Shakes.Count > 10)
            {
                return BadRequest("Exceeded maximum order limit (10 shakes per order).");
            }
            // Create the order
            var newOrder = new Order(); // You may add discounts here
            // Record the time when the order is received
            newOrder.OrderCreationTime = DateTime.Now;

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

            // Set the total price for the order
            newOrder.TotalPrice = totalPrice;
            newOrder.CustomerName = request.ClientName;
            // Set the order date as the current date and time
            newOrder.OrderDate = DateTime.Today;
           
            // Record the time when the order is processed
            newOrder.OrderPreparationEndTime = DateTime.Now;

            // Calculate the time taken to process the order
            // TimeSpan orderProcessingTime = orderProcessedTime - orderReceivedTime;
            // Generate a unique ID for the order
            newOrder.OrderID = Guid.NewGuid();
            // Send an HTTP POST request to save the order in the database using the OrdersController
            var response = await _httpClient.PostAsJsonAsync("", newOrder);

            if (response.IsSuccessStatusCode)
            {
                // Order saved successfully in the database
                return Ok("Order saved successfully!");
            }
            // Handle the case if the order saving fails
            return BadRequest("Failed to save the order.");

            //return Ok(newOrder); // Return the created order or save it in the database
        }

    }

   
}
