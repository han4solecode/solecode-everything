namespace OnlineFoodOrderingSystem
{
    public class Order
    {
        // Order class will be able to store MenuItem's
        // it can be stored with a List<MenuItem>
        // use private member access modifier so the list cannot be
        // accessed outside this class
        private readonly List<MenuItem> menuItemList = [];
        private int totalPrice;
        private string status;

        // Order class constructor
        public Order(List<MenuItem> menuItems,  string status)
        {
            menuItemList = menuItems;
            this.status = status;
        }

        public List<MenuItem> MenuItemList
        {
            get => menuItemList;
        }
        public int TotalPrice { get => totalPrice; }
        public string Status
        {
            get => status;
            set => status = value;
        }

        // method for calculate total price of order
        public int CalculateTotal()
        {
            totalPrice = 0;
            foreach (MenuItem menu in menuItemList)
            {
                totalPrice += menu.CalucatePrice();
            }

            return totalPrice;
        }
    }
}