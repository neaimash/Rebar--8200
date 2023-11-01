using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace Rebar.Models
{
    public class EndDayReport
    {
        public static string ManagerPassword = "6783395";
        [BsonElement("DateDay")]
        public DateTime date { get; set; }
        [BsonElement("SumOrders")]
        public int SumOrders { get; set; } = 0;
        [BsonElement("SumMoney")]
        public int SumMoney { get; set; } = 0;
        public EndDayReport()
        {

        }
    }
}
