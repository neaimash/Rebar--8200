namespace Rebar.Model
{
    public class Menu
    {
        public List<MenuShake> menuShakes { get; private set; }
        public Menu()
        {
            menuShakes = new List<MenuShake>();
        }
        // Method to add a new shake to the menu
        
        public void AddShake(string name, string description, int priceSmall, int priceMedium, int priceLarge)
        {
            MenuShake newShake = new MenuShake();
            menuShakes.Add(newShake);
        }
  

        public MenuShake GetShakeByID(Guid shakeID)
         {
             return menuShakes.Find(shake => shake.ID == shakeID);
         }
       
    }
}
