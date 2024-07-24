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