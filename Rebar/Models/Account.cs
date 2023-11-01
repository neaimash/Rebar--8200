using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Rebar.Model
{
    public class Account
    {
        public List<Order> Orders { get; set; }
       
        public decimal TotalOrdersPrice;
        public Account()
        {
            Orders = new List<Order>();
            TotalOrdersPrice = 0;
        }
        public void AddOrderToAcount(Order o)
        {
            try
            {
                Orders.Add(o);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }
    }
}
