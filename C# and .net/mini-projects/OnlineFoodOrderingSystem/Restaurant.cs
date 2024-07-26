namespace OnlineFoodOrderingSystem
{
    public class Restaurant
    {
        // Restaurant fields
        private readonly string? name;
        private readonly List<MenuItem> menuList = [];
        private List<Order> orders = []; 
        private int revenue;

        // Restaurant props
        public string? Name
        {
            get => name;
        }
        public List<Order> Orders
        {
            get => orders;
            set => orders = value;
        }
        
        // Restaurant constructor
        public Restaurant(string name)
        {
            this.name = name;
        }

        // method for adding new item to menu
        public void AddItemToMenu(MenuItem menu)
        {
            menuList.Add(menu);   
            Console.WriteLine("{0} is added to the {1} menu", menu.Name, name);
        }

        // method for receiving orders
        public void ReceiveOrders(Order order)
        {
            Orders.Add(order);
            Console.WriteLine("Order added");
        }

        // method for calculating total revenue
        public int CalculateRevenue()
        {
            revenue = 0;
            foreach (Order o in Orders)
            {
                revenue += o.CalculateTotal();
            }

            return revenue;
        }
    }
}