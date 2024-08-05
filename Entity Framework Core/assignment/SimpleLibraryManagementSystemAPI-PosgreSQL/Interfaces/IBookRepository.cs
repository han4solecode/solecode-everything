using SimpleLibraryManagementSystemAPI_PosgreSQL.Models;

namespace SimpleLibraryManagementSystemAPI_PosgreSQL.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetBookbyId(int id);
        void CreateBook(Book book);
        void UpdateBook(Book inputBook);
        void DeleteBook(int id);
        void Save();
    }
}