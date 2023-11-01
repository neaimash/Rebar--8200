using Rebar.Model;
using static Rebar.Model.MenuShake;

namespace Rebar.Models
{
    public class OrderShake
    {
        public Guid ID{ get; }= Guid.NewGuid(); // Generate a new unique GUID
        public MenuShake OrderedShake { get;private set; }
        public ShakeSize OrderedSize { get; set; }
        public decimal Price { get; set; }
        public OrderShake()
        {
            
        }
        public OrderShake(MenuShake shake, ShakeSize size)
        {
            OrderedShake = shake;
            OrderedSize = size;
            Price = shake.GetPrice(size); // Calculate the price based on the Shake and its size
        }

    }
}
