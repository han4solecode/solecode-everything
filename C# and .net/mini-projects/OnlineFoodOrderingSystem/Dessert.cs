namespace OnlineFoodOrderingSystem
{
    public class Dessert : MenuItem
    {
        // Dessert prop
        public string? SugarLevel { get; set; }

        // overriding CalculatePrice method from MenuItem abstract class
        public override int CalucatePrice()
        {
            throw new NotImplementedException();
        }
    }
}