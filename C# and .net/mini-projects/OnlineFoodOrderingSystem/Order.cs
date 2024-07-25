namespace OnlineFoodOrderingSystem
{
    public class Order
    {
        // Order class will be able to store MenuItem's
        // it can be stored with a List<MenuItem>
        // use private member access modifier so the list cannot be
        // accessed outside this class
        private List<MenuItem> menuItemList = [];
        private int totalPrice;

        // Order class constructor
        public Order(List<MenuItem> menuItems, int totalPrice)
        {
            this.menuItemList = menuItems;
            this.totalPrice = totalPrice;
        }

        public List<MenuItem> MenuItemList { get; }
        public int TotalPrice { get; }

        // method for calculate total price of order
        public int CalculateTotal()
        {
            foreach (MenuItem menu in menuItemList)
            {
                totalPrice += menu.CalucatePrice();
            }

            return totalPrice;
        }
    }
}