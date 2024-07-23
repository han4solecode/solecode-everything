namespace SimpleLibraryManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        // create new library
        var myLibrary = new Library();

        // create new five books
        var book1 = new Book("Book 1", "Author 1", 2019);
        var book2 = new Book("Book 2", "Author 2", 2020);
        var book3 = new Book("Book 3", "Author 3", 2021);
        var book4 = new Book("Book 4", "Author 4", 2022);
        var book5 = new Book("Book 5", "Author 5", 2023);

        // add those five books to library
        myLibrary.AddBook(book1);
        myLibrary.AddBook(book2);
        myLibrary.AddBook(book3);
        myLibrary.AddBook(book4);
        myLibrary.AddBook(book5);

        // add book that already exist
        myLibrary.AddBook(book5);

        // remove Book 3 from library
        myLibrary.RemoveBook("Book 3");

        // remove book that doesn't exist
        myLibrary.RemoveBook("Book 9");

        // display all books available in library
        myLibrary.DisplayBooks();

    }
}

public class Book(string title, string author, int publicationYear)
{
    // getter props for reading field value
    public string Title
    {
        get { return title; }
    }
    public string Author
    {
        get { return author; }
    }
    public int PublicationYear
    {
        get { return publicationYear; }
    }

}

public class Library()
{
    List<Book> bookList = [];

    public void AddBook(Book book)
    {
        // check if book already exist
        if (bookList.Contains(book))
        {
            Console.WriteLine("{0} already exist in the library", book.Title);
        }
        else
        {
            Console.WriteLine("Adding {0} to library", book.Title);
            bookList.Add(book);
        }
    }

    public void RemoveBook(string bookTitle)
    {
        // get only book with desired title
        var bookToRemove = bookList.SingleOrDefault(b => b.Title == bookTitle);
        // check if book title exist
        if (bookToRemove != null)
        {
            bookList.Remove(bookToRemove);
            Console.WriteLine("{0} has been removed from library", bookToRemove.Title);
        }
        else
        {
            Console.WriteLine("{0} does not exist in the library", bookTitle);
        }
    }

    public void DisplayBooks()
    {
        Console.WriteLine("===== Available Books ====");
        // display with for loop
        for (int i = 0; i < bookList.Count; i++)
        {
            Console.WriteLine("Book Title: {0}\nBook Author: {1}\nPublication Year: {2}\n--------------------------", bookList[i].Title, bookList[i].Author, bookList[i].PublicationYear);
        }
    }

}