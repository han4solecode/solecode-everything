using SLMS.Domain.Entities;

namespace SLMS.Application.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();

        Task<Book?> GetBookById(int bookId);

        Task AddBook(Book book);

        Task<Book?> UpdateBook(int bookId, Book inputBook);

        Task<bool> DeleteBook(int bookId);
    }
}