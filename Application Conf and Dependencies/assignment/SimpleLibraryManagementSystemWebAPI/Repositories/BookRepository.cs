using Microsoft.EntityFrameworkCore;
using SimpleLibraryManagementSystemWebAPI.Data;
using SimpleLibraryManagementSystemWebAPI.Interfaces;
using SimpleLibraryManagementSystemWebAPI.Models;

namespace SimpleLibraryManagementSystemWebAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext libraryContext)
        {
            _context = libraryContext;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();

            return books;
        }

        public async Task<Book?> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            return book;
        }

        public async Task AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book?> UpdateBook(int id, Book inputBook)
        {
            var bookToBeUpdated = await _context.Books.FindAsync(id);

            if (bookToBeUpdated == null)
            {
                return null;
            }

            bookToBeUpdated.Title = inputBook.Title;
            bookToBeUpdated.Author = inputBook.Author;
            bookToBeUpdated.Publicationyear = inputBook.Publicationyear;
            bookToBeUpdated.Isbn = inputBook.Isbn;

            _context.Books.Update(bookToBeUpdated);
            await _context.SaveChangesAsync();

            return bookToBeUpdated;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var bookToBeDeleted = await _context.Books.FindAsync(id);

            if (bookToBeDeleted == null)
            {
                return false;
            }

            _context.Books.Remove(bookToBeDeleted);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}