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
            MenuShake newShake = new MenuShake(name, description, priceSmall, priceMedium, priceLarge);
            menuShakes.Add(newShake);
        }

        // Method to retrieve all shakes in the menu
        public List<MenuShake> GetAllShakes()
        {
            return menuShakes;
        }
        public void UpdateShake(Guid shakeID, string newName, string newDescription, int newPriceSmall, int newPriceMedium, int newPriceLarge)
        {
            MenuShake shakeToUpdate = menuShakes.Find(shake => shake.ID == shakeID);
            if (shakeToUpdate != null)
            {
                shakeToUpdate.Name = newName;
                shakeToUpdate.Description = newDescription;
                shakeToUpdate.PriceSmall = newPriceSmall;
                shakeToUpdate.PriceMedium = newPriceMedium;
                shakeToUpdate.PriceLarge = newPriceLarge;
            }
        }

        public void DeleteShake(Guid shakeID)
        {
            MenuShake shakeToDelete = menuShakes.Find(shake => shake.ID == shakeID);
            if (shakeToDelete != null)
            {
                menuShakes.Remove(shakeToDelete);
            }
        }



        public MenuShake GetShakeByID(Guid shakeID)
         {
             return menuShakes.Find(shake => shake.ID == shakeID);
         }
       
    }
}
