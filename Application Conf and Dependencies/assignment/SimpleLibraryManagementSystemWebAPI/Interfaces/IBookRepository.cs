using SimpleLibraryManagementSystemWebAPI.Models;

namespace SimpleLibraryManagementSystemWebAPI.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();

        Task<Book?> GetBookById(int id);

        Task AddBook(Book book);

        Task<Book?> UpdateBook(int id, Book inputBook);

        Task<bool> DeleteBook(int id);
    }
}