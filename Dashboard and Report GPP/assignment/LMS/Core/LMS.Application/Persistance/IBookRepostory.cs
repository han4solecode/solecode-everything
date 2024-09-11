using LMS.Application.DTOs;
using LMS.Application.Persistance.Common;
using LMS.Application.Persistance.Helper;
using LMS.Domain.Entities;

namespace LMS.Application.Persistance
{
    public interface IBookRepostory : IBaseRepository<Book>
    {
        Task<IEnumerable<Book>> SearchBook(QueryObject query, int recordsPerPage, int currentPage);

        Task BookSoftDelete(Book book, string? reason);

        Task<IEnumerable<Book>> GetAllNoPaging();

        Task<int> GetBookCount();

        // Task<IEnumerable<Book>> GetOverdueBooks();
    }
}