namespace OnlineFoodOrderingSystem
{
    public class Dessert : MenuItem
    {
        // Dessert prop
        public string? SugarLevel { get; set; }

        // Dessert constructor
        public Dessert(string sugarLevel, string name, int price, string description): base(name, price, description)
        {
            SugarLevel = sugarLevel;
        }

        // overriding CalculatePrice method from MenuItem abstract class
        public override int CalucatePrice()
        {
            return price;
        }

        // overriding MenuInfo method from MenuItem abstract class
        public override string MenuInfo()
        {
            return "Name: " + name + "\nPrice: " + price + "\nDescription: " + description + "\nSugar Level: " + SugarLevel;
        }
    }
}