using Microsoft.EntityFrameworkCore;
using SLMS.Application.Repositories;
using SLMS.Domain.Entities;
using SLMS.Persistance.Data;
using SLMS.Persistance.Factories;

namespace SLMS.Persistance.Repositories
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

        public async Task<Book?> GetBookById(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);

            return book;
        }

        public async Task AddBook(Book book)
        {
            var newBook = new BookFactory().CreateItem(book);
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
        }

        public async Task<Book?> UpdateBook(int bookId, Book inputBook)
        {
            var bookToBeUpdated = await _context.Books.FindAsync(bookId);

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

        public async Task<bool> DeleteBook(int bookId)
        {
            var bookToBeDeleted = await _context.Books.FindAsync(bookId);

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