namespace OnlineFoodOrderingSystem
{
    // This class inherits MenuItem absract class
    // We have to implement all abstract method from MenuItem through overriding it and return desired outcome
    public class Food : MenuItem
    {
        // Food prop
        public string? Spiciness { get; set; }

        // Food constructor
        // Using base keyword so that all fields from MenuItem is inherited in the class constructor and we still be able to add prop unique to this class
        public Food(string spiciness, string name, int price, string description): base(name, price, description)
        {
            Spiciness = spiciness;
        }

        // overriding CalculatePrice method from MenuItem abstract class
        public override int CalucatePrice()
        {
            return price;
        }

        // overriding MenuInfo method from MenuItem abstract class
        public override string MenuInfo()
        {
            return "Name: " + name + "\nPrice: " + price + "\nDescription: " + description + "\nSpiciness: " + Spiciness;
        }
    }
}