namespace OnlineFoodOrderingSystem;

class Program
{
    static void Main(string[] args)
    {
        bool isDone = false;
        int selection;

        // create new OnlineFoodOrderingSystem object
        var system = new OnlineFoodOrderingSystem();

        // create new Restaurant objects
        var restaurant1 = new Restaurant("Restaurant 1");
        var restaurant2 = new Restaurant("Restaurant 2");
        var restaurant3 = new Restaurant("Restaurant 3");

        // add restaurants to system using AddRestaurant() method
        system.AddRestaurant(restaurant1);
        system.AddRestaurant(restaurant2);
        system.AddRestaurant(restaurant3);

        // create new MenuItem object, Food, Beverage, and Dessert
        var food1 = new Food("Level 2", "Empal Gentong", 25000, "Kinda like beef soup with coconut milk");
        var beverage1 = new Beverage("Large", "Es Teh", 6000, "Iced Tea from the best tea farm");
        var dessert1 = new Dessert("High", "Klepon", 2000, "Sweet rice cake ball filled with molten palm sugar and coated in grated coconut");

        var food2 = new Food("Level 5", "Seblak", 12000, "Spicy and savoury wet krupuk");
        var beverage2 = new Beverage("Small", "Espresso", 9000, "Medium dark roasted beans");
        var dessert2 = new Dessert("Low", "Lemon Sorbet", 10000, "When life gives you lemon, make lemon sorbet?");

        var food3 = new Food("Level 3", "Bibimbap", 35000, "Korean mixed rice bowl");
        var beverage3 = new Beverage("Medium", "Lemonade", 17000, "The best lemonade in town");
        var dessert3 = new Dessert("High", "Vanilla Ice Cream", 7000, "3 cups of vanilla ice cream");

        // add items to restaurant menu
        restaurant1.AddItemToMenu(food1);
        restaurant1.AddItemToMenu(beverage1);
        restaurant1.AddItemToMenu(dessert1);

        restaurant2.AddItemToMenu(food2);
        restaurant2.AddItemToMenu(beverage2);
        restaurant2.AddItemToMenu(dessert2);

        restaurant3.AddItemToMenu(food3);
        restaurant3.AddItemToMenu(beverage3);
        restaurant3.AddItemToMenu(dessert3);

        List<MenuItem> order1 = [food1, food3, beverage3, beverage1, dessert3];
        List<MenuItem> order2 = [food2, food3, beverage2, beverage1, dessert2, dessert1];
        List<MenuItem> order3 = [food1, beverage1, dessert2, dessert1, dessert3];
        List<MenuItem> order4 = [food1, food2, food3, beverage1, beverage2, beverage3, dessert1, dessert2, dessert3];

        // place orders to restaurants
        system.PlaceOrder("Restaurant 1", order1);
        system.PlaceOrder("Restaurant 2", order2);
        system.PlaceOrder("Restaurant 2", order3);
        system.PlaceOrder("Restaurant 3", order4);

        while (!isDone)
        {
            Console.WriteLine("1: Display all order info");
            Console.WriteLine("2: Cancel an order");
            Console.WriteLine("3: Check order status");
            Console.WriteLine("4: Check restaurant revenue");
            Console.WriteLine("0: Exit");
            Console.Write("What you wanna do? ");
            selection = Convert.ToInt16(Console.ReadLine());

            switch (selection)
            {
                case 0:
                    Console.WriteLine("Okay, good bye!");
                    isDone = true;
                    break;
                case 1:
                    foreach (string orderNumber in system.PlacedOrderInRestaurant.Keys)
                    {
                        system.DisplayOrderDetails(orderNumber);
                    }
                    break;
                case 2:
                    foreach (string orderNumber in system.PlacedOrderInRestaurant.Keys)
                    {
                        Console.WriteLine("Order number: {0}", orderNumber);
                    }
                    Console.Write("Input order number you want to cancel: ");
                    string? orderNumberToCancel = Console.ReadLine();
                    if (orderNumberToCancel != null)
                    {
                        system.CancelOrder(orderNumberToCancel);
                    }
                    break;
                case 3:
                    foreach (string orderNumber in system.PlacedOrderInRestaurant.Keys)
                    {
                        Console.WriteLine("Order number: {0}", orderNumber);
                    }
                    Console.Write("Input order number you want to check the status: ");
                    string? orderNumberToCheckStatus = Console.ReadLine();
                    if (orderNumberToCheckStatus != null)
                    {
                        system.GetOrderStatus(orderNumberToCheckStatus);
                    }
                    break;
                case 4:
                    foreach (Restaurant resto in system.RestaurantList)
                    {
                        Console.WriteLine("{0} revenue: {1}", resto.Name, resto.CalculateRevenue().ToString());
                    }
                    break;
            }
        }
    }
}
