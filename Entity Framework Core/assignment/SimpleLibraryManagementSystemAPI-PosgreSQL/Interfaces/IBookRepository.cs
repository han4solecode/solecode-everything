using SimpleLibraryManagementSystemAPI_PosgreSQL.Models;

namespace SimpleLibraryManagementSystemAPI_PosgreSQL.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetBookbyId(int id);
        Book CreateBook(Book book);
        Book? UpdateBook(int id, Book inputBook);
        bool DeleteBook(int id);
        // void Save();
    }
}