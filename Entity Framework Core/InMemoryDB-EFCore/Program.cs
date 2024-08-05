using Microsoft.EntityFrameworkCore;

namespace InMemoryDB_EFCore;

class Program
{
    static void Main(string[] args)
    {
        // create a new Purchase item and store it in In Memory DB
        using (var context  = new AppDBContext())
        {
            var newPurchase = new Purchase()
            {
                // Id = 1,
                Product = "Shirt",
                Price = 20.25m
            };

            var anotherPurchase = new Purchase()
            {
                // Id = 1,
                Product = "Socks",
                Price = 10.29m
            };

            context.Purchases.Add(newPurchase);
            context.Purchases.Add(anotherPurchase);

            // don't forget to save bois
            context.SaveChanges();

            // get all Purchase data
            var getAllPurchases = context.Purchases.ToList();

            // remove a Purchase data
            var purchaseToBeDeleted = context.Purchases.FirstOrDefault(p => p.Id == 1);

            if (purchaseToBeDeleted != null)
            {
                context.Purchases.Remove(purchaseToBeDeleted);
                context.SaveChanges();
            }

            var p = context.Purchases.ToList();
        }
    }

    // DB context class
    public class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyDB");
        }

        // prop for Purchase model in DBContext
        public DbSet<Purchase> Purchases { get; set; }
    }

    // Puchase model
    public class Purchase
    {
        public int Id { get; set; }

        public string? Product { get; set; }

        public decimal Price { get; set; }
    }
}
