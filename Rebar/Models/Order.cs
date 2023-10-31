using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Rebar.Models;
using System;
using System.Collections.Generic;

namespace Rebar.Model
{
    public class Order
    {
        [BsonId]
        public Guid OrderID { get; set; } 
        //public List<OrderShake> Shakes { get; set; }
        public List<Guid> ShakesID { get; set; }
        [BsonElement("TotalPrice")]
        public decimal TotalPrice { get; set; }
        [BsonElement("CustomerName")]
        public string CustomerName { get; set; }
        [BsonElement("OrderDate")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime OrderCreationTime { get; set; }
        public DateTime OrderPreparationEndTime { get; set; }
        public List<Discount> DiscountsAndPromotions { get; set; }

        /*public Order()
        {
            TotalPrice = CalculateTotalPrice();
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
        */
    }
}
