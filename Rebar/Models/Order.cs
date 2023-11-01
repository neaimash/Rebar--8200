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
        public Guid OrderID { get; set; } = Guid.NewGuid();
        public List<OrderShake> Shakes { get; set; }
      
        [BsonElement("TotalPrice")]
        public decimal TotalPrice { get; set; }
        [BsonElement("CustomerName")]
        public string CustomerName { get; set; }
        [BsonElement("OrderDate")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [BsonElement("OrderCreationTime")]
        public DateTime OrderCreationTime { get; set; }
        [BsonElement("OrderPreparationEndTime")]
        public DateTime OrderPreparationEndTime { get; set; }
        public List<Discount> DiscountsAndPromotions { get; set; }

         public Order()
        {
            TotalPrice = 0;
        }

        
    }
}
