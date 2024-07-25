namespace OnlineFoodOrderingSystem
{
    public class Food : MenuItem
    {
        // Food prop
        public string? Spiciness { get; set; }

        // overriding CalculatePrice method from MenuItem abstract class
        public override int CalucatePrice()
        {
            throw new NotImplementedException();
        }
    }
}