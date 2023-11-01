namespace Rebar.Model
{
    public class Menu
    {
        public List<MenuShake> menuShakes { get; private set; }

        public Menu()
        {
            menuShakes = new List<MenuShake>();
        }

       
        public void AddMenuShake(MenuShake newShake)
        {
            menuShakes.Add(newShake);
        }

        public MenuShake GetShakeByID(Guid shakeID)
        {
            return menuShakes.Find(shake => shake.ID == shakeID);
        }

        public MenuShake GetShakeByName(string name)
        {
            return menuShakes.Find(shake => shake.Name == name);
        }
    }
}
