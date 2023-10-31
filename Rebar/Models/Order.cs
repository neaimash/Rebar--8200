using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Rebar.Models;

namespace Rebar.Model
{
public class Order
{
    [BsonId]
    public Guid OrderID { get; set; }
    public List<OrderShake> Shakes { get; set; }
    [BsonElement("TotalPrice")]
    public decimal TotalPrice { get; set; }
    [BsonElement("CustomerName")]
    public string CustomerName { get; set; }
    [BsonElement("OrderDate")]
    public DateTime OrderDate { get; set; }
    public List<Discount> DiscountsAndPromotions { get; set; }

    public Order(List<OrderShake> shakes, string customerName, List<Discount> discountsAndPromotions)
    {
        Shakes = shakes;
        TotalPrice = CalculateTotalPrice();
        OrderID = Guid.NewGuid();
        CustomerName = customerName;
        OrderDate = DateTime.Now;
        DiscountsAndPromotions = discountsAndPromotions;
    }

    public decimal CalculateTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (var shake in Shakes)
        {
            totalPrice += shake.Price;
        }
        return totalPrice;
    }

}
}



