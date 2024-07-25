namespace OnlineFoodOrderingSystem
{
    public class Restaurant
    {
        // Restaurant props
        public string? Name { get; set; }
        public required List<MenuItem> Menu { get; set; }
        public required List<Order> Orders { get; set; }

        private int revenue;

        // method for adding new item to menu
        public void AddItemToMenu(MenuItem menu)
        {
            Menu.Add(menu);
            Console.WriteLine("{0} is added to the menu", menu.Name);
        }

        // method for receiving orders
        public void ReceiveOrders(Order order)
        {
            Orders.Add(order);
            Console.WriteLine("Order added");
        }

        // method for calculating total revenue
        public int CalculateRevenue(Order order)
        {
            foreach (Order o in Orders)
            {
                revenue += o.CalculateTotal();
            }

            return revenue;
        }
    }
}