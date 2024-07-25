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
            this.name = name;
            this.price = price;
            this.description = description;
        }

        // overriding CalculatePrice method from MenuItem abstract class
        public override int CalucatePrice()
        {
            throw new NotImplementedException();
        }
    }
}