using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Rebar.Model
{
    public class MenuShake
    {
        [BsonId]
        public Guid ID { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("PriceSmall")]
        public int PriceSmall { get; set; }
        [BsonElement("PriceMedium")]
        public int PriceMedium { get; set; }
        [BsonElement("PriceLarge")]
        public int PriceLarge { get; set; }
       
        public enum ShakeSize
        {
            Small,
            Medium,
            Large
        }

        public MenuShake(string name, string description, int priceSmall, int priceMedium, int priceLarge)
        {
            ID = Guid.NewGuid(); // Unique ID generation
            Name = name;
            Description = description;
            PriceSmall = priceSmall;
            PriceMedium = priceMedium;
            PriceLarge = priceLarge;

        }

        public decimal GetPrice(ShakeSize size)
        {
            switch (size)
            {
                case ShakeSize.Small:
                    return PriceSmall;
                case ShakeSize.Medium:
                    return PriceMedium;
                case ShakeSize.Large:
                    return PriceLarge;
                default:
                    return 0;
            }
        }


    }
}