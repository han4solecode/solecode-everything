namespace OnlineFoodOrderingSystem
{
    public class Food : MenuItem
    {
        // Food prop
        public string? Spiciness { get; set; }

        // Food constructor
        public Food(string spiciness, string name, int price, string description): base(name, price, description)
        {
            Spiciness = spiciness;
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