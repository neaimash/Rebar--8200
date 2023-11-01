using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Rebar.Model
{
    public class Account
    {
        public List<Order> Orders { get; set; }
       
        public decimal TotalOrdersPrice;
        public Account(List<Order> orders)
        {
            Orders = orders;
            TotalOrdersPrice = calculateTotalOrdersPrice();
        }
        public decimal calculateTotalOrdersPrice()
        {
            decimal totalOrdersPrice = 0;
            foreach (var order in Orders)
            {
                totalOrdersPrice += order.TotalPrice;
            }
            return totalOrdersPrice;
        }
    }
}
