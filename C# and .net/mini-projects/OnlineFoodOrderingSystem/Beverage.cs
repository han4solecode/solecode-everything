namespace OnlineFoodOrderingSystem
{
    public class Beverage : MenuItem
    {
        // Beverage prop
        public string? Size { get; set; }

        // Beverage constructor
        public Beverage(string size, string name, int price, string description): base(name, price, description)
        {
            this.Size = size;
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