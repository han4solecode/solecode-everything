namespace OnlineFoodOrderingSystem
{
    public abstract class MenuItem
    {
        // Every class that will inherit MenuItem abstract class will inherit these props.
        // Encapsulate fields with protected member access modifier so it can only be accessed by class that inherit MenuItem.
        // Properties is used as an accessor to these private fields 
        protected string? name;
        protected int price;
        protected string? description;

        public string? Name
        {
            get => name;
            set => name = value;
        }
        public int Price
        {
            get => price;
            set => price = value;
        }
        public string? Description
        {
            get => description;
            set => description = value;
        }

        protected MenuItem(string? name, int price, string description)
        {
            this.name = name;
            this.price = price;
            this.description = description;
        }

        // abstract method to calculate price
        public abstract int CalucatePrice();

        // abstract method to display menu info
        public abstract string MenuInfo();

    }
}