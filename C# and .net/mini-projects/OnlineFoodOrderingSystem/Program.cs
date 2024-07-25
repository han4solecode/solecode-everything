namespace OnlineFoodOrderingSystem;

class Program
{
    static void Main(string[] args)
    {
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
        restaurant2.AddItemToMenu(dessert3);

        restaurant3.AddItemToMenu(food3);
        restaurant3.AddItemToMenu(beverage3);
        restaurant3.AddItemToMenu(dessert3);

        // order simulation
        // list of item to be ordered
        List<MenuItem> order1 = [food1, food3, beverage3, beverage1, dessert3];
        // placing order to system, returning an order number if successfull
        string orderNumber1 = system.PlaceOrder("Bakmi GM", order1); // <- invalid restaurat name, will return error message
        Console.WriteLine(orderNumber1);

        string orderNumber2 = system.PlaceOrder("Restaurant 2", order1); // <- order is valid, will return order number
        Console.WriteLine("Order Number: {0}", orderNumber2);

        // display order through order number
        system.DisplayOrderDetails("123"); // <- invalid order number
        system.DisplayOrderDetails(orderNumber2); // <- valid order number
        
    }
}
