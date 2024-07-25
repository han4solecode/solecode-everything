namespace OnlineFoodOrderingSystem
{
    public class Beverage : MenuItem
    {
        // Beverage prop
        public string? Size { get; set; }

        // overriding CalculatePrice method from MenuItem abstract class
        public override int CalucatePrice()
        {
            throw new NotImplementedException();
        }

    }
}