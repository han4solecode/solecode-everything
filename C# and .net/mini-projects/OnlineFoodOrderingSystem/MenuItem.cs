namespace OnlineFoodOrderingSystem
{
    public abstract class MenuItem
    {
        // Every class that will inherit MenuItem abstract class will inherit these props.
        // Encapsulate fields with private member access modifier so it can not be accessed directly outside this class.
        // Properties is used as an accessor to these private fields 
        private string? name;
        private int price;
        private string? description;

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

        public MenuItem(string name, int price, string desc)
        {
            this.name = name;
            this.price = price;
            description = desc;
        }

        // abstract method to calculate price
        public abstract int CalucatePrice();

    }
}