namespace OnlineFoodOrderingSystem
{
    public abstract class MenuItem
    {
        // every class that will inherit MenuItem abstract class
        // will inherit these props
        // creating props with this syntax, we don't need to declare
        // any field that associated with these props, it automatically
        // encapsulate the field as a private variable and we will
        // be able to access it through these props
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }

        // method
        public abstract int CalucatePrice();

    }
}