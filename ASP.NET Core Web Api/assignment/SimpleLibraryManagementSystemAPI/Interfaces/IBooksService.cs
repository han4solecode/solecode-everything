using SimpleLibraryManagementSystemAPI.Models;

namespace SimpleLibraryManagementSystemAPI.Interfaces
{
    public interface IBooksService
    {
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
        Book CreateBook(Book book);
        Book? UpdateBook(int id, Book inputBook);
        bool DeleteBook(int id);
    }
}