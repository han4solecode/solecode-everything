namespace SimpleLibraryManagementSystem
{
    public class Library()
    {
        private List<Book> bookList = [];

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
}