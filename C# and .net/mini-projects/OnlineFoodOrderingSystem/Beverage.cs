namespace OnlineFoodOrderingSystem
{
    public class Beverage : MenuItem
    {
        // Beverage prop
        public string? Size { get; set; }

        // Beverage constructor
        public Beverage(string size, string name, int price, string description): base(name, price, description)
        {
            Size = size;
        }

        // overriding CalculatePrice method from MenuItem abstract class
        public override int CalucatePrice()
        {
            return price;
        }

        // overriding MenuInfo method from MenuItem abstract class
        public override string MenuInfo()
        {
            return "Name: " + name + "\nPrice: " + price + "\nDescription: " + description + "\nSize: " + Size;
        }

    }
}