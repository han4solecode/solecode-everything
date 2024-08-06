using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore_CodeFirst;

class Program
{
    static void Main(string[] args)
    {
        var _context = new AppDbContext();

        var newBook = new Book()
        {
            Title = "Clean Coder",
            Author = "Uncle Bob",
            ISBN = "12345678",
            Price = 20000
        };

        var updatedBook = new Book()
        {
            Id = 1,
            Title = "Clean Code",
            Author = "Robert",
            ISBN = "87654321",
            Price = 25000
        };

        var anotherBook = new Book()
        {
            Title = "asd",
            Author = "dsa",
            ISBN = "923187",
            Price = 15000
        };

        var deleteBook = new Book()
        {
            Id = 2,
            Title = "asd",
            Author = "dsa",
            ISBN = "923187",
            Price = 15000
        };

        // _context.Books.Add(anotherBook);
        // _context.Books.Update(updatedBook);
        // _context.Books.Remove(deleteBook);
        // _context.SaveChanges();

        var allBooks = _context.Books.ToList();

        foreach (var item in allBooks)
        {
            Console.WriteLine($"{item.Id} | {item.Title} | {item.Author} | {item.ISBN} | {item.Price}");
        }

    }
}
