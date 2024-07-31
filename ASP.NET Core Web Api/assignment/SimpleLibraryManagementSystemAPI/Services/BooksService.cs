using SimpleLibraryManagementSystemAPI.Interfaces;
using SimpleLibraryManagementSystemAPI.Models;

namespace SimpleLibraryManagementSystemAPI.Services
{
    public class BooksService : IBooksService
    {
        private static List<Book> books = [];

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public Book? GetBookById(int id)
        {
            var book = books.FirstOrDefault(b => b.ID == id);

            if (book == null)
            {
                return null;
            }

            return book;
        }

        public Book CreateBook(Book book)
        {
            books.Add(book);
            return book;
        }

        public Book? UpdateBook(int id, Book inputBook)
        {
            var book = GetBookById(id);

            if (book != null)
            {
                book.ID = inputBook.ID;
                book.Title = inputBook.Title;
                book.Author = inputBook.Author;
                book.PublicationYear = inputBook.PublicationYear;
                book.ISBN = inputBook.ISBN;

                return book;
            }

            return null;
        }

        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);

            if (book == null)
            {
                return false;
            }

            books.Remove(book);
            return true;
        }
    }
}