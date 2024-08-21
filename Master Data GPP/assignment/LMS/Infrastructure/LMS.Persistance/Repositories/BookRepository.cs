using LMS.Application.Persistance;
using LMS.Application.Persistance.Helper;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistance.Repositories
{
    public class BookRepository : IBookRepostory
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(Book entity)
        {
            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Book entity)
        {
            _context.Books.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAll(int recordsPerPage, int currentPage)
        {
            var books = await _context.Books.Where(b => b.IsDeleted == false).Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).OrderBy(b => b.Title).ToListAsync();
            return books;
        }

        public async Task<Book?> GetById(int id)
        {
            var book = await _context.Books.Where(b => b.IsDeleted == false).SingleOrDefaultAsync(b => b.Id == id);
            return book;
        }

        public async Task Update(Book entity)
        {
            _context.Books.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> SearchBook(QueryObject query, int recordsPerPage, int currentPage)
        {
            if (query.GetType().GetProperties().Select(q => q.GetValue(query)).All(val => val == null))
            {
                return await GetAll(recordsPerPage, currentPage);
            }

            var books = _context.Books.AsQueryable();

            var res = await books.Where(b => 
                ((!string.IsNullOrEmpty(query.Author) && b.Author.Contains(query.Author)) ||
                (!string.IsNullOrEmpty(query.Category) && b.Category!.Contains(query.Category)) ||
                (!string.IsNullOrEmpty(query.ISBN) && b.ISBN.Contains(query.ISBN)) ||
                (!string.IsNullOrEmpty(query.Language) && b.Language!.Contains(query.Language)) ||
                (!string.IsNullOrEmpty(query.Title) && b.Title.Contains(query.Title))) && b.IsDeleted == false
            ).Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).OrderBy(b => b.Title).ToListAsync();

            return res;
        }

        public async Task BookSoftDelete(Book book, string? reason)
        {
            book.IsDeleted = true;
            book.DateDeleted = DateOnly.FromDateTime(DateTime.Now);
            book.DeleteReason = reason;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}