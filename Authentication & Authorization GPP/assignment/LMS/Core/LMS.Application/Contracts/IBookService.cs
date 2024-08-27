using LMS.Application.Persistance.Helper;
using LMS.Domain.Entities;

namespace LMS.Application.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks(int a, int b);

        Task<Book?> GetBookById(int id);

        Task<bool> AddNewBook(Book book);

        Task<bool> UpdateExistingBook(int id, Book inputBook);

        Task<bool> DeleteExistingBook(int id);

        Task<IEnumerable<Book>> SearchBookByQuery(QueryObject query, int a, int b);

        Task<bool> BookSoftDelete(int id, string? reason);
    }
}